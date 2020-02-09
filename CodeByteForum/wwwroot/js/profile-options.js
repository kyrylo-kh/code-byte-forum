var emailChangeButton = document.getElementById("email-change-btn");
var passwordChangeButton = document.getElementById("password-change-btn");
var emailChangeForm = document.getElementById("email-change-form");
var passwordChangeForm = document.getElementById("password-change-form");
var save = document.getElementById("save");
var saveDiv = document.getElementById("save-div");
emailChangeButton.addEventListener("click", () => {
    if (emailChangeForm.style.display == "block") {
        emailChangeForm.style.display = "none";
    }
    else {
        emailChangeForm.style.display = "block";
    }
    showOrHideSaveBut();
});
passwordChangeButton.addEventListener("click", () => {
    if (passwordChangeForm.style.display == "block")
        passwordChangeForm.style.display = "none";
    else
        passwordChangeForm.style.display = "block";
    showOrHideSaveBut();
});
var NP = document.getElementById("NP");
var CP = document.getElementById("CP");
NP.addEventListener("input", Check);
CP.addEventListener("input", Check);

function Check() {
    if (!(NP.value == CP.value)) {
        var CPS = document.getElementById("CPS");
        CPS.innerText = "Пароли не совпадают";
        save.disabled = true;
        console.log(NP.value + " " + CP.value);
    }
    else if (NP.value == CP.value) {
        var CPS = document.getElementById("CPS");
        CPS.innerText = "";
        console.log(NP.value + " " + CP.value);
        save.disabled = false;
    }
}

function showOrHideSaveBut() {
    if (passwordChangeForm.style.display == "block" || emailChangeForm.style.display == "block")
        saveDiv.style.display = "block";
    else
        saveDiv.style.display = "none";
}

var email = document.getElementById("User_Email");
email.addEventListener("input", () => {
    email.value = email.value;
    console.log(email.value);
})