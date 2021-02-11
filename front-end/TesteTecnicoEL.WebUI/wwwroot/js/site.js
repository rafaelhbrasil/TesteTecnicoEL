// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function alternaSomenteAtivos(checkbox) {
    if (document.getElementById("ver-somente-disponiveis").checked)
        $(".indisponivel").hide();
    else
        $(".indisponivel").show();
}

function CarregarSelecaoVeiculo(id) {
    window.location.href = URL_VEICULO_SELECIONADO;
}

function SubmeterAcesso() {
    var cpf = $("#formSignInContainer #CPF").val();
    var senha = $("#formSignInContainer #Senha").val();
    $.post(URL_ACESSO,
        {
            cpf: cpf,
            senha: senha
        }).done(function (data) {
            if (data) {
                $("#formSignInContainer").html(data);
            }
            else {
                $("#fechar-modal-cadastro").click();
                RecarregarDadosUsuarioHeader();
            }
        });
}

function SubmeterCadastro() {
    var nome = $("#formSignUpContainer #Nome").val();
    var cpf = $("#formSignUpContainer #CPF").val();
    var nascimento = $("#formSignUpContainer #Nascimento").val();
    var senha = $("#formSignUpContainer #Senha").val();
    var logradouro = $("#formSignUpContainer #Endereco_Logradouro").val();
    var numero = $("#formSignUpContainer #Endereco_Numero").val();
    var complemento = $("#formSignUpContainer #Endereco_Complemento").val();
    var cidade = $("#formSignUpContainer #Endereco_Cidade").val();
    var estado = $("#formSignUpContainer #Endereco_Estado").val();
    $.post(URL_CADASTRO,
        {
            nome: nome,
            cpf: cpf,
            nascimento: nascimento,
            senha: senha,
            "Endereco.Logradouro": logradouro,
            "Endereco.Numero": numero,
            "Endereco.Complemento": complemento,
            "Endereco.Cidade": cidade,
            "Endereco.Estado": estado
        }).done(function (data) {
            if (data) {
                $("#formSignUpContainer").html(data);
            }
            else {
                $("#fechar-modal-cadastro").click();
                RecarregarDadosUsuarioHeader();
            }
        });
}

function AlternarFormAcessoCadastro(campoParaExibir) {
    $('#div-frm-login').hide();
    $('#div-frm-signup').hide();
    $('#' + campoParaExibir).show();
}

function recoverPassword() {
    alert("Recuperação de senha não está no escopo do teste.");
}

function RecarregarDadosUsuarioHeader() {
    $("#fechar-modal-cadastro").click();
    $.get(URL_LOGIN_HEADER)
        .done(function (data) {
            $("#container-dados-usuario-header").html(data);
        });
}