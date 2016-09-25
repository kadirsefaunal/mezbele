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

    // kayıt form
    $('.ui.form').form({
        fields: {
            username: {
                identifier: 'username',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Lütfen kullanıcı adınızı girin.'
                    }
                ]
            },
            email: {
                identifier: 'email',
                rules: [
                  {
                      type: 'empty',
                      prompt: 'Lütfen e-posta adresi girin.'
                  },
                  {
                      type: 'email',
                      prompt: 'Lütfen geçerli bir e-posta adresi giriniz.'
                  }
                ]
            },
            password: {
                identifier: 'password',
                rules: [
                  {
                      type: 'empty',
                      prompt: 'Lütfen parolanızı girin.'
                  },
                  {
                      type: 'length[6]',
                      prompt: 'Parolanız en az 6 haneli olmalıdır.'
                  }
                ]
            }
        }
    });
});

function showSignUpModal() {
    $('#signUpModal').modal('show');
}

function showLoginModal() {
    $('#loginModal').modal('show');
}