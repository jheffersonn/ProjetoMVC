﻿using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Context;
using ProjetoMVC.Models;
using System.Linq;

namespace ProjetoMVC.Controllers
{
    public class ContatoController : Controller
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contatos = _context.Contatos.ToList();
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        
        }

        [HttpPost]
        public IActionResult Criar(Contato contato) 
        { 
            if (ModelState.IsValid) 
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(contato);
        }

        public IActionResult Editar(int id) 
        {

            //var contato = _context.Contatos.FirstOrDefault(x => x.Id == id);
            var contato = _context.Contatos.Find(id);
            if (contato == null)
                //NotFound();
                return RedirectToAction(nameof(Index));

            return View(contato);
        
        }

        [HttpPost]
        public IActionResult Editar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));

        }
    }
}
