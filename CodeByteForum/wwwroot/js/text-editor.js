let textarea = document.getElementById("text");

function pasteTag(word) {
    if (word == 'b') {
        textarea.value += "<b></b>";
        setCaretToPos(textarea, textarea.value.length - 4);
    }
    if (word == 'h1') {
        textarea.value += "<h1></h1>";
        setCaretToPos(textarea, textarea.value.length - 5);
    }
    if (word == 'h2') {
        textarea.value += "<h2></h2>";
        setCaretToPos(textarea, textarea.value.length - 5);
    }
    if (word == 'h3') {
        textarea.value += "<h3></h3>";
        setCaretToPos(textarea, textarea.value.length - 5);
    }
    if (word == 'h4') {
        textarea.value += "<h4></h4>";
        setCaretToPos(textarea, textarea.value.length - 5);
    }
}
textarea.addEventListener("keydown", (e) => {
    switch (e.keyCode) {
        case 66:
            if (e.ctrlKey)
                pasteTag('b');
            break;
    }
});
function setCaretToPos(input, pos) {
    setSelectionRange(input, pos, pos);
}

function setSelectionRange(input, selectionStart, selectionEnd) {
    if (input.setSelectionRange) {
        input.focus();
        input.setSelectionRange(selectionStart, selectionEnd);
    }
}

