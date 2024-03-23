using System.Collections.Generic;
using System.Web.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using Newtonsoft.Json;
using System.Web.Services.Description;
using WebGrease;
using FloristeriaMillan.Querys.Logica_Usuarios;
using FloristeriaMillan.Querys.Logica_CanalUsuarios;
using System.Linq;

namespace Stripe
{

    public class StripeController : Controller
    {

        [HttpPost]
        public ActionResult CreateCheckoutSession()
        {
            try
            {
                string emailCli = "";
                Boolean bandera = false;


                if (Session["Usuario"] != null)
                {
                    emailCli = (string)Session["Usuario"];
                    bandera = true;
                }

                if (Session["Default"] != null)
                {
                    emailCli = (String)Session["Default"];
                }

                LO_CanalUsuariosController query = new LO_CanalUsuariosController();

                CarritoProducto[] carrito = query.buscar_carrito(emailCli);

                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>()
                };

                foreach (var item in carrito)
                {
                    long precioUnitario = (long)(item.producto.precioPro * 100);
                    long cantidad = (long)item.carrito.cantidadCar;

                    var lineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = precioUnitario,
                            Currency = "mxn",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.producto.nombrePro,
                                //Images = new List<string> { item.producto.fotoPro },
                            },
                        },
                        Quantity = cantidad,
                    };

                    options.LineItems.Add(lineItem);
                }

                options.Mode = "payment";
                if (bandera)
                {
                    options.CustomerEmail = emailCli;
                }
                options.SuccessUrl = "http://localhost:44394/HomeUsuario/Sucess";

                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);
                return new HttpStatusCodeResult(303);
            }
            catch (Exception)
            {
                return RedirectToAction("Carrito", "HomeUsuario");
            }
        }
    }

}