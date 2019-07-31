﻿using goldStore.Areas.Panel.Models.Repository;
using goldStore.Areas.Panel.Models.Security;
using goldStore.Models.ViewModel;
using goldStore.Areas.Panel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Configuration;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace goldStore.Controllers
{
    public class UserController : Controller
    {
        CategoryRepository repoCategory = new CategoryRepository(new Areas.Panel.Models.goldstoreEntities());
        BrandRepository repoBrand = new BrandRepository(new Areas.Panel.Models.goldstoreEntities());
        UserRepository repoUser = new UserRepository(new Areas.Panel.Models.goldstoreEntities());
       
        // GET: User
        public PartialViewResult PartialBrands()
        {
            // Tüm markaları partial View'e Gönder
            return PartialView(repoBrand.GetAll());
        }
        public PartialViewResult PartialNewArrivals()
        {
            return PartialView();
        }

        public PartialViewResult PartialPrice()
        {
            return PartialView();
        }

        public PartialViewResult PartialCategory()
        {
            return PartialView(repoCategory.GetAll());
        }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register register)
        {
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                if (isExistUser(register.email))
                {
                    message = "Bu maile kayıtlı bir kullanıcı mevcuttur.";
                    ViewBag.message = message;
                    return View();
                }
                user user = new user();
                user.email = register.email;
                // parolalar şifreleniyor
                user.password = Crypto.Hash(register.password);
                user.rePassword = Crypto.Hash(register.comfirmPassword);
                // aktivasyon kodu üretiyoruz.
                user.activationCode = Guid.NewGuid().ToString();
                user.roleId = 2;
                // oluşturulan kullanıcının mail doğrulması başlangıçta olsun.
                user.isMailVerified = false;

                user.createdDate = DateTime.Now;
                //Kaydet
                repoUser.Save(user);
                // mail gönder
                SendVerificationLinkEmail(user.email, user.activationCode);

                message = "Kaydınız başarılı şekilde gerçekleştirildi. Hesap aktivasyon linki "
                     + user.email + " adresinize gönderilmiştir.Doğrulamayı unutmayınız.";
                status = true;
                ViewBag.message = message;
                ViewBag.status = status;
            }
            return View();
        }

        [NonAction]
        public bool isExistUser(string username)
        {
            var user = repoUser.GetAll().Where(a => a.email == username).FirstOrDefault();
            return user == null ? false : true;
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            SmtpSection network = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                var verifyUrl = "/User/VerifyAccount/" + activationCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                var fromEmail = new MailAddress(network.Network.UserName, "Goldstore Üyeliği");
                var toEmail = new MailAddress(emailID);

                string subject = "Goldstore sitemize Hoşgeldiniz!";
                string body = "<br/><br/>Goldstore hesabınız başarıyla oluşturulmuştur. Hesabınız aktive etmek için aşağıdaki linke tıklayınız" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
                var smtp = new SmtpClient
                {
                    Host = network.Network.Host,
                    Port = network.Network.Port,
                    EnableSsl = network.Network.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = network.Network.DefaultCredentials,
                    Credentials = new NetworkCredential(network.Network.UserName, network.Network.Password)
                };
                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}