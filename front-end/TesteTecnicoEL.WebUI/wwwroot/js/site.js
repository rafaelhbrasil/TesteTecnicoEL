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
