"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.RawTouchscreenImpl = exports.RawMouseImpl = exports.RawKeyboardImpl = void 0;

var input = _interopRequireWildcard(require("../input"));

var _macEditingCommands = require("../macEditingCommands");

var _utils = require("../../utils");

var _crProtocolHelper = require("./crProtocolHelper");

function _getRequireWildcardCache(nodeInterop) { if (typeof WeakMap !== "function") return null; var cacheBabelInterop = new WeakMap(); var cacheNodeInterop = new WeakMap(); return (_getRequireWildcardCache = function (nodeInterop) { return nodeInterop ? cacheNodeInterop : cacheBabelInterop; })(nodeInterop); }

function _interopRequireWildcard(obj, nodeInterop) { if (!nodeInterop && obj && obj.__esModule) { return obj; } if (obj === null || typeof obj !== "object" && typeof obj !== "function") { return { default: obj }; } var cache = _getRequireWildcardCache(nodeInterop); if (cache && cache.has(obj)) { return cache.get(obj); } var newObj = {}; var hasPropertyDescriptor = Object.defineProperty && Object.getOwnPropertyDescriptor; for (var key in obj) { if (key !== "default" && Object.prototype.hasOwnProperty.call(obj, key)) { var desc = hasPropertyDescriptor ? Object.getOwnPropertyDescriptor(obj, key) : null; if (desc && (desc.get || desc.set)) { Object.defineProperty(newObj, key, desc); } else { newObj[key] = obj[key]; } } } newObj.default = obj; if (cache) { cache.set(obj, newObj); } return newObj; }

/**
 * Copyright 2017 Google Inc. All rights reserved.
 * Modifications copyright (c) Microsoft Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the 'License');
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an 'AS IS' BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
class RawKeyboardImpl {
  constructor(_client, _isMac, _dragManger) {
    this._client = _client;
    this._isMac = _isMac;
    this._dragManger = _dragManger;
  }

  _commandsForCode(code, modifiers) {
    if (!this._isMac) return [];
    const parts = [];

    for (const modifier of ['Shift', 'Control', 'Alt', 'Meta']) {
      if (modifiers.has(modifier)) parts.push(modifier);
    }

    parts.push(code);
    const shortcut = parts.join('+');
    let commands = _macEditingCommands.macEditingCommands[shortcut] || [];
    if ((0, _utils.isString)(commands)) commands = [commands]; // Commands that insert text are not supported

    commands = commands.filter(x => !x.startsWith('insert')); // remove the trailing : to match the Chromium command names.

    return commands.map(c => c.substring(0, c.length - 1));
  }

  async keydown(modifiers, code, keyCode, keyCodeWithoutLocation, key, location, autoRepeat, text) {
    if (code === 'Escape' && (await this._dragManger.cancelDrag())) return;

    const commands = this._commandsForCode(code, modifiers);

    await this._client.send('Input.dispatchKeyEvent', {
      type: text ? 'keyDown' : 'rawKeyDown',
      modifiers: (0, _crProtocolHelper.toModifiersMask)(modifiers),
      windowsVirtualKeyCode: keyCodeWithoutLocation,
      code,
      commands,
      key,
      text,
      unmodifiedText: text,
      autoRepeat,
      location,
      isKeypad: location === input.keypadLocation
    });
  }

  async keyup(modifiers, code, keyCode, keyCodeWithoutLocation, key, location) {
    await this._client.send('Input.dispatchKeyEvent', {
      type: 'keyUp',
      modifiers: (0, _crProtocolHelper.toModifiersMask)(modifiers),
      key,
      windowsVirtualKeyCode: keyCodeWithoutLocation,
      code,
      location
    });
  }

  async sendText(text) {
    await this._client.send('Input.insertText', {
      text
    });
  }

}

exports.RawKeyboardImpl = RawKeyboardImpl;

class RawMouseImpl {
  constructor(page, client, dragManager) {
    this._client = void 0;
    this._page = void 0;
    this._dragManager = void 0;
    this._page = page;
    this._client = client;
    this._dragManager = dragManager;
  }

  async move(x, y, button, buttons, modifiers, forClick) {
    const actualMove = async () => {
      await this._client.send('Input.dispatchMouseEvent', {
        type: 'mouseMoved',
        button,
        buttons: (0, _crProtocolHelper.toButtonsMask)(buttons),
        x,
        y,
        modifiers: (0, _crProtocolHelper.toModifiersMask)(modifiers)
      });
    };

    if (forClick) {
      // Avoid extra protocol calls related to drag and drop, because click relies on
      // move-down-up protocol commands being sent synchronously.
      return actualMove();
    }

    await this._dragManager.interceptDragCausedByMove(x, y, button, buttons, modifiers, actualMove);
  }

  async down(x, y, button, buttons, modifiers, clickCount) {
    if (this._dragManager.isDragging()) return;
    await this._client.send('Input.dispatchMouseEvent', {
      type: 'mousePressed',
      button,
      buttons: (0, _crProtocolHelper.toButtonsMask)(buttons),
      x,
      y,
      modifiers: (0, _crProtocolHelper.toModifiersMask)(modifiers),
      clickCount
    });
  }

  async up(x, y, button, buttons, modifiers, clickCount) {
    if (this._dragManager.isDragging()) {
      await this._dragManager.drop(x, y, modifiers);
      return;
    }

    await this._client.send('Input.dispatchMouseEvent', {
      type: 'mouseReleased',
      button,
      buttons: (0, _crProtocolHelper.toButtonsMask)(buttons),
      x,
      y,
      modifiers: (0, _crProtocolHelper.toModifiersMask)(modifiers),
      clickCount
    });
  }

  async wheel(x, y, buttons, modifiers, deltaX, deltaY) {
    await this._client.send('Input.dispatchMouseEvent', {
      type: 'mouseWheel',
      x,
      y,
      modifiers: (0, _crProtocolHelper.toModifiersMask)(modifiers),
      deltaX,
      deltaY
    });
  }

}

exports.RawMouseImpl = RawMouseImpl;

class RawTouchscreenImpl {
  constructor(client) {
    this._client = void 0;
    this._client = client;
  }

  async tap(x, y, modifiers) {
    await Promise.all([this._client.send('Input.dispatchTouchEvent', {
      type: 'touchStart',
      modifiers: (0, _crProtocolHelper.toModifiersMask)(modifiers),
      touchPoints: [{
        x,
        y
      }]
    }), this._client.send('Input.dispatchTouchEvent', {
      type: 'touchEnd',
      modifiers: (0, _crProtocolHelper.toModifiersMask)(modifiers),
      touchPoints: []
    })]);
  }

}

exports.RawTouchscreenImpl = RawTouchscreenImpl;