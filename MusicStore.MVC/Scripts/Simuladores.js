var Tasa = -1;
var ValorMinimo = -1;
var ValorMaximo = -1;
var PlazoMaximoQuincenal = -1;
var PlazoMaximoMensual = -1;
var PorcentajeSeguro = -1;
var CuotaUnica = false;

function ObtenerParametroCredito(Codigo) {
    var Ruta = '/Simulador/ObtenerDetallesParametroCredito?CodigoParametro=';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = $(location).attr('protocol') + '//' + $(location).attr('host') + $(location).attr('pathname') + "/ObtenerDetallesParametroCredito?CodigoParametro="
    }

    Ruta = Ruta.replace('/Oportunidades/NuevaOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Oportunidades/EditarOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Solicitud/NuevaSolicitud', '/Simulador');
    Ruta = Ruta.replace('/Oportunidades/NuevaSolicitudOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Simulador/SimuladorCredito', '/Simulador');

    $.ajax({
        url: Ruta + Codigo,
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (result) {
            Tasa = result.Tasa;
            CuotaUnica = result.CuotaUnica;
            PorcentajeSeguro = result.PorcentajeSeguro;
            ValorMinimo = result.ValorMinimo;
            ValorMaximo = result.ValorMaximo;
            PlazoMaximoQuincenal = result.PlazoMaximoQuincenal;
            PlazoMaximoMensual = result.PlazoMaximoMensual;

            $('#PeriodoPago').prop('selectedIndex', -1);
            $('#Plazo').val('');

            $('#MensajeValor').empty();
            $('#MensajeValor').text('El valor a solicitar debe estar entre los rangos de ' + result.ValorMinimo + " a " + result.ValorMaximo)
            $('#MensajeValor').show();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function CalcularSimuladorCredito(ValorCredito, Periodos, Plazo, Tasa, Seguro) {
    var Quincenal = true;

    switch (Periodos) {
        case '1':
            {
                Quincenal = false;
                break;
            }
        case '2':
            {
                Quincenal = true;
                break;
            }
    }

    var Ruta = '/Simulador/CalcularValorCuotaMensual?ValorCredito=' + ValorCredito + '&ValorTasa=' + Tasa + '&Periodos=' + Plazo + '&PorcentajeSeguro=' + Seguro + '&PeriodosQuincenales=' + Quincenal;
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = $(location).attr('protocol') + '//' + $(location).attr('host') + $(location).attr('pathname') + '/CalcularValorCuotaMensual?ValorCredito=' + ValorCredito + '&ValorTasa=' + Tasa + '&Periodos=' + Plazo + '&PorcentajeSeguro=' + Seguro + '&PeriodosQuincenales=' + Quincenal;
    }

    Ruta = Ruta.replace('/Oportunidades/NuevaOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Oportunidades/EditarOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Solicitud/NuevaSolicitud', '/Simulador');
    Ruta = Ruta.replace('/Oportunidades/NuevaSolicitudOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Simulador/SimuladorCredito', '/Simulador');

    $.ajax({
        url: Ruta,
        type: 'GET',
        dataType: 'json',
        success: function (result) {

            try {
                $('#PlanPagos').children('tbody').html('');
                for (var i = 0; i <= result.PlanAmortizacion.length - 1; i++) {
                    $('#PlanPagos').children('tbody').append('<tr><td>' + result.PlanAmortizacion[i].Periodo + '</td><td>' + result.PlanAmortizacion[i].Valor + '</td><td>' + result.PlanAmortizacion[i].Capital + '</td><td>' + result.PlanAmortizacion[i].Intereses + '</td><td>' + result.PlanAmortizacion[i].SaldoCapital + '</td><td>' + result.PlanAmortizacion[i].SaldoIntereses + '</td></tr>');
                }

                $('#PlanPagos').children('tfoot').html('');
                $('#PlanPagos').children('tfoot').append('<tr><td><strong>Totales</strong></td><td><strong>' + result.ValorTotalCredito + '</strong></td><td><strong>' + result.ValorCapital + '</strong></td><td><strong>' + result.ValorIntereses + '</strong></td><td></td><td></td></tr>');
            }
            catch (err)
            { alert(err); }

            $('#TasaPeriodica').val(result.Tasa);
            $('#ValorIntereses').val(result.ValorIntereses);
            $('#ValorCuota').val(result.ValorCuota);
            $('#ValorSeguro').val(result.ValorSeguro);
            $('#ValorAdministracion').val(result.ValorAdministracion);
            $('#ValorTotal').val(result.ValorTotalCredito);
            $('#Resultado').show();
            $('#Contactar').show();

        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function CalcularSimuladorCreditoCuotaFija(ValorCredito, FechaPago, Tasa, Seguro) {
    var Ruta = '/Simulador/CalcularValorCuotaFija?ValorCredito=' + ValorCredito + '&ValorTasa=' + Tasa + '&FechaPago=' + FechaPago + '&PorcentajeSeguro=' + Seguro;
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = $(location).attr('protocol') + '//' + $(location).attr('host') + $(location).attr('pathname') + '/CalcularValorCuotaFija?ValorCredito=' + ValorCredito + '&ValorTasa=' + Tasa + '&FechaPago=' + FechaPago + '&PorcentajeSeguro=' + Seguro;
    }

    Ruta = Ruta.replace('/Oportunidades/NuevaOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Oportunidades/EditarOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Solicitud/NuevaSolicitud', '/Simulador');
    Ruta = Ruta.replace('/Oportunidades/NuevaSolicitudOportunidad', '/Simulador');
    Ruta = Ruta.replace('/Simulador/SimuladorCredito', '/Simulador');

    $.ajax({
        url: Ruta,
        type: 'GET',
        dataType: 'json',
        success: function (result) {

            try {
                $('#PlanPagos').children('tbody').html('');
                for (var i = 0; i <= result.PlanAmortizacion.length - 1; i++) {
                    $('#PlanPagos').children('tbody').append('<tr><td>' + result.PlanAmortizacion[i].Periodo + '</td><td>' + result.PlanAmortizacion[i].Valor + '</td><td>' + result.PlanAmortizacion[i].Capital + '</td><td>' + result.PlanAmortizacion[i].Intereses + '</td><td>' + result.PlanAmortizacion[i].SaldoCapital + '</td><td>' + result.PlanAmortizacion[i].SaldoIntereses + '</td></tr>');
                }

                $('#PlanPagos').children('tfoot').html('');
                $('#PlanPagos').children('tfoot').append('<tr><td><strong>Totales</strong></td><td><strong>' + result.ValorTotalCredito + '</strong></td><td><strong>' + result.ValorCapital + '</strong></td><td><strong>' + result.ValorIntereses + '</strong></td><td></td><td></td></tr>');
            }
            catch (err)
            { alert(err); }

            $('#TasaPeriodica').val(result.Tasa);
            $('#ValorIntereses').val(result.ValorIntereses);
            $('#ValorCuota').val(result.ValorCuota);
            $('#ValorSeguro').val(result.ValorSeguro);
            $('#ValorTotal').val(result.ValorTotalCredito);
            $('#Resultado').show();
            $('#Contactar').show();

        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}