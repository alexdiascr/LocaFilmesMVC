using System.Threading.Tasks;
using LocaFilme.Business.Interfaces;
//using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace LocaFilme.App.Extensions
{

    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        //Sempre retorna uma view 
        //O InvokeAsync vai pegar as notificações e transforma em uma lista
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Pegando todas notificações
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());


            //ViewData guarda vários estado, inclusive a ModelState
            //E colcoando na ModelState como se fosse um erro na model, ou seja, vai ser tratado 
            //no formulário como se fosse um erro de preenchimento de campo, mas sem campo específico
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

            return View(); //Substituir o item que mostra as notificações
        }
    }
}