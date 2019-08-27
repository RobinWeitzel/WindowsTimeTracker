const keyCodes = {
    "FF": "no VK mapping",
    "1E": "Accept",
    "5D": "Context Menu",
    "A6": "Browser Back",
    "AB": "Browser Favorites",
    "A7": "Browser Forward",
    "AC": "Browser Home",
    "A8": "Browser Refresh",
    "AA": "Browser Search",
    "A9": "Browser Stop",
    "14": "Caps Lock",
    "1C": "Convert",
    "2E": "Delete",
    "28": "Arrow Down",
    "23": "End",
    "70": "F1",
    "79": "F10",
    "7A": "F11",
    "7B": "F12",
    "7C": "F13",
    "7D": "F14",
    "7E": "F15",
    "7F": "F16",
    "80": "F17",
    "81": "F18",
    "82": "F19",
    "71": "F2",
    "83": "F20",
    "84": "F21",
    "85": "F22",
    "86": "F23",
    "87": "F24",
    "72": "F3",
    "73": "F4",
    "74": "F5",
    "75": "F6",
    "76": "F7",
    "77": "F8",
    "78": "F9",
    "18": "Final",
    "2F": "Help",
    "24": "Home",
    "E4": "Ico00 *",
    "2D": "Insert",
    "17": "Junja",
    "15": "Kana",
    "19": "Kanji",
    "B6": "App1",
    "B7": "App2",
    "B4": "Mail",
    "B5": "Media",
    "01": "Left Button **",
    "A2": "Left Ctrl",
    "25": "Arrow Left",
    "A4": "Left Alt",
    "A0": "Left Shift",
    "5B": "Left Win",
    "04": "Middle Button **",
    "B0": "Next Track",
    "B3": "Play / Pause",
    "B1": "Previous Track",
    "B2": "Stop",
    "1F": "Mode Change",
    "22": "Page Down",
    "1D": "Non Convert",
    "90": "Num Lock",
    "92": "Jisho",
    "13": "Pause",
    "2A": "Print",
    "21": "Page Up",
    "02": "Right Button **",
    "A3": "Right Ctrl",
    "27": "Arrow Right",
    "A5": "Right Alt",
    "A1": "Right Shift",
    "5C": "Right Win",
    "91": "Scrol Lock",
    "5F": "Sleep",
    "2C": "Print Screen",
    "26": "Arrow Up",
    "AE": "Volume Down",
    "AD": "Volume Mute",
    "AF": "Volume Up",
    "05": "X Button 1 **",
    "06": "X Button 2 **",
    "C1": "Abnt C1",
    "C2": "Abnt C2",
    "6B": "Numpad +",
    "F6": "Attn",
    "08": "Backspace",
    "03": "Break",
    "0C": "Clear",
    "F7": "Cr Sel",
    "6E": "Numpad .",
    "6F": "Numpad /",
    "F9": "Er Eof",
    "1B": "Esc",
    "2B": "Execute",
    "F8": "Ex Sel",
    "E6": "IcoClr",
    "E3": "IcoHlp",
    "30": "0",
    "31": "1",
    "32": "2",
    "33": "3",
    "34": "4",
    "35": "5",
    "36": "6",
    "37": "7",
    "38": "8",
    "39": "9",
    "41": "A",
    "42": "B",
    "43": "C",
    "44": "D",
    "45": "E",
    "46": "F",
    "47": "G",
    "48": "H",
    "49": "I",
    "4A": "J",
    "4B": "K",
    "4C": "L",
    "4D": "M",
    "4E": "N",
    "4F": "O",
    "50": "P",
    "51": "Q",
    "52": "R",
    "53": "S",
    "54": "T",
    "55": "U",
    "56": "V",
    "57": "W",
    "58": "X",
    "59": "Y",
    "5A": "Z",
    "6A": "Numpad *",
    "FC": "NoName",
    "60": "Numpad 0",
    "61": "Numpad 1",
    "62": "Numpad 2",
    "63": "Numpad 3",
    "64": "Numpad 4",
    "65": "Numpad 5",
    "66": "Numpad 6",
    "67": "Numpad 7",
    "68": "Numpad 8",
    "69": "Numpad 9",
    "BA": "OEM_1 (: ;)",
    "E2": "OEM_102 (> <)",
    "BF": "OEM_2 (? /)",
    "C0": "OEM_3 (~ `)",
    "DB": "OEM_4 ({ [)",
    "DC": "OEM_5 (| \\)",
    "DD": "OEM_6 (} ])",
    "DE": "OEM_7 (\" ')",
    "DF": "OEM_8 (§ !)",
    "F0": "Oem Attn",
    "F3": "Auto",
    "E1": "Ax",
    "F5": "Back Tab",
    "FE": "OemClr",
    "BC": "OEM_COMMA (< ,)",
    "F2": "Copy",
    "EF": "Cu Sel",
    "F4": "Enlw",
    "F1": "Finish",
    "95": "Loya",
    "93": "Mashu",
    "96": "Roya",
    "94": "Touroku",
    "EA": "Jump",
    "BD": "OEM_MINUS (_ -)",
    "EB": "OemPa1",
    "EC": "OemPa2",
    "ED": "OemPa3",
    "BE": "OEM_PERIOD (> .)",
    "BB": "OEM_PLUS (+ =)",
    "E9": "Reset",
    "EE": "WsCtrl",
    "FD": "Pa1",
    "E7": "Packet",
    "FA": "Play",
    "E5": "Process",
    "0D": "Enter",
    "29": "Select",
    "6C": "Separator",
    "20": "Space",
    "6D": "Num -",
    "09": "Tab",
    "FB": "Zoom"
};

const keyCodesToHex = {
    8: "08",
    9: "09",
    13: "0D",
    16: "A0",
    17: "A2",
    18: "A4",
    19: "B3",
    20: "14",
    27: "1B",
    33: "21",
    34: "22",
    35: "23",
    36: "24",
    37: "25",
    38: "26",
    39: "27",
    40: "28",
    45: "2D",
    46: "2E",
    48: "30",
    49: "31",
    50: "32",
    51: "33",
    52: "34",
    53: "35",
    54: "36",
    55: "37",
    56: "38",
    57: "39",
    65: "41",
    66: "42",
    67: "43",
    68: "44",
    69: "45",
    70: "46",
    71: "47",
    72: "48",
    73: "49",
    74: "4A",
    75: "4B",
    76: "4C",
    77: "4D",
    78: "4E",
    79: "4F",
    80: "50",
    81: "51",
    82: "52",
    83: "53",
    84: "54",
    85: "55",
    86: "56",
    87: "57",
    88: "58",
    89: "59",
    90: "5A",
    91: "01",
    92: "02",
    93: "29",
    96: "60",
    97: "61",
    98: "62",
    99: "63",
    100: "64",
    101: "65",
    102: "66",
    103: "67",
    104: "68",
    105: "69",
    106: "6A",
    107: "6B",
    109: "6D",
    110: "6E",
    111: "6F",
    112: "f1",
    113: "f2",
    114: "f3",
    115: "f4",
    116: "f5",
    117: "f6",
    118: "f7",
    119: "f8",
    120: "f9",
    121: "f10",
    122: "f11",
    123: "f12",
    144: "90",
    145: "91",
    186: "BA",
    187: "BB",
    188: "BC",
    189: "BD",
    190: "BE",
    191: "A7",
    220: "A6"
}

TSFRepository.registerComponent(class ViewSettings extends TSFComponent {
    constructor() {
        super();

        this.display = 'block';

        this.keys = [];
        this.keysRendered = [];

        this.getSettings().then(settings => {
            this.state.darkMode = settings.darkMode;
            document.documentElement.setAttribute("dark_mode", this.state.darkMode);
            this.state.timeNotificationVisible = settings.timeNotificationVisible;
            this.state.playNotificationSound = settings.playNotificationSound;
            this.state.timeBeforeAskingAgain = settings.timeBeforeAskingAgain;
            this.state.timeSinceAppLastUsed = settings.timeSinceAppLastUsed;
            this.state.hotkeyDisabled = settings.hotkeyDisabled;
            this.keysRendered = settings.hotkeys.map(k => k.toString(16).length === 1 ? "0" + k.toString(16).toUpperCase(): k.toString(16).toUpperCase());
            this.hotkey_render_output();
        });
    }

    async getSettings() {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getSettings().then(result => {
                resolve(JSON.parse(result));
            });
        });
    }

    async setSettings() {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.setSettings({
                DarkMode: this.state.darkMode,
                TimeNotificationVisible: this.state.timeNotificationVisible,
                PlayNotificationSound: this.state.playNotificationSound,
                TimeBeforeAskingAgain: this.state.timeBeforeAskingAgain,
                TimeSinceAppLastUsed: this.state.timeSinceAppLastUsed,
                HotkeyDisabled: this.state.hotkeyDisabled,
                Hotkeys: this.keysRendered.map(k => parseInt(k, 16))
            }).then(result => {
                resolve(JSON.parse(result));
            });
        });
    }

    darkModeClick(e) {
        if(e.target.nodeName !== "DIV") {
            this.state.darkMode = !this.state.darkMode;
            document.documentElement.setAttribute("dark_mode", this.state.darkMode);
        }
    }

    playNotificationSoundClick(e) {
        if(e.target.nodeName !== "DIV") {
            this.state.playNotificationSound = !this.state.playNotificationSound;
        }
    }

    hotkeyEnabledClick(e) {
        if(e.target.nodeName !== "DIV") {
            this.state.hotkeyDisabled = !this.state.hotkeyDisabled;
        }
    }

    hotkey_on_key_down(event) {
        event.preventDefault();
        const char = keyCodesToHex[event.which || event.keyCode] || (event.which || event.keyCode);

        if (this.keys.length === 0) { // new hotkey input
            this.keys = [];
            this.keysRendered = [];
        }

        const i = this.keys.indexOf(char);

        if (i < 0) {
            this.keys.push(char);
            this.keysRendered.push(char);
        }

        this.hotkey_render_output();
    }

    hotkey_on_key_up(event) {
        event.preventDefault();
        const char = keyCodesToHex[event.which || event.keyCode] || (event.which || event.keyCode);

        const i = this.keys.indexOf(char);

        if (i >= 0) {
            this.keys.splice(i, 1);
        }
    }

    hotkey_on_key_press(event) {
        return false;
    }

    hotkey_render_output() {
        let value = [];

        for (const key of this.keysRendered) {
            value.push(keyCodes[key] || String.fromCharCode(key));
        }

        this.state.hotkeys = value.join(" + ");
    }
});