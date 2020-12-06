using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Web.Helpers
{
    public static class IJSExtensions
    {
        public async static Task<object> MostrarMensajes(this IJSRuntime js, string mensaje)
        {
            return await js.InvokeAsync<object>("Swal.fire", mensaje);
        }

        public async static Task<object> MostrarMensajes(this IJSRuntime js, string titulo, string mensajes, TiposDeAlertas tiposDeAlertas)
        {
            return await js.InvokeAsync<object>("Swal.fire", titulo, mensajes,tiposDeAlertas.ToString());
        }

        public async static Task<bool> Comfirmar(this IJSRuntime js, string titulo, string mensajes, TiposDeAlertas tiposDeAlertas)
        {
            return await js.InvokeAsync<bool>("customComfirm", titulo, mensajes, tiposDeAlertas.ToString());
        }

        public async static Task<bool> AvisoAlert(this IJSRuntime js, string titulo,  string tiempo, string position, TiposDeAlertas tiposDeAlertas)
        {
            return await js.InvokeAsync<bool>("avisoAlert", titulo,position, tiempo, tiposDeAlertas.ToString());
        }


    }

    public enum TiposDeAlertas
    {
        question, warning, error, success, info

    }
}
