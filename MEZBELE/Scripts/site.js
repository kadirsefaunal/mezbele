$(document).ready(function () {
    // fixed menu
    $('.masthead').visibility({
        once: false,
        onBottomPassed: function () {
            $('.fixed.menu').transition('fade in');
        },
        onBottomPassedReverse: function () {
            $('.fixed.menu').transition('fade out');
        }
    });

    // sidebar
    $('.ui.sidebar').sidebar('attach events', '.toc.item');

});

function showSignUpModal() {
    $('#signUpModal').modal('show');
}

function showLoginModal() {
    $('#loginModal').modal('show');
}