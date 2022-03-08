function Init() {
    if (document.cookie == "") {
        document.cookie = "questionnaire=0; path=/";
    }


    var questionnaireValue = document.cookie.slice(document.cookie.indexOf("questionnaire"), document.cookie.length).split("=");

    if (parseInt(questionnaireValue[1]) == 0) {
        Swal.fire({
            icon: 'success',
            title: `<h3 style="font-size: 1em; font-family: 'Quicksand', sans-serif; font-weight: bold;">Welcome on the α version!</h3>`,
            html:
                `<h3 style="font-size: 1em; font-family: 'Quicksand', sans-serif; font-weight: 600;">` +
                `You're currently the α version of PyStach.io and it may contain some bugs. <br />` +
                `Feel free to visit and try everything here. You also have all my socials and a contact form if you have any question, advice ... etc. <br />` +
                `Have a nice trip!` +
                `</h3>`,
            showCloseButton: false,
            showDenyButton: false
        });
        document.cookie = "questionnaire=1; path=/";
    }
}