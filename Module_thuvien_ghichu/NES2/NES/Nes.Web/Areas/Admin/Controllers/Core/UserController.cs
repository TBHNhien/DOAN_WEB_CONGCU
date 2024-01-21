using log4net;
using Nes.Common;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Admin/User/
         [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<Nes.Dal.EntityModels.User>().All();
            return View(model);
        }


        //
        // GET: /Admin/User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = User.Identity.Name;
                if (ModelState.IsValid)
                {
                   
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        unitOfWork.GetRepository<User>().Create(user);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminCreateRecordSuccess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorValidForm);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(user);
        }

        //
        // GET: /Admin/User/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string userName)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            User user = null;
            try
            {
                user = unitOfWork.GetRepository<User>().GetById(userName);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(user);
        }

        //
        // POST: /Admin/User/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        user.UpdatedDate = DateTime.Now;
                        user.UpdatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<User>().Update(user);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminEditRecordSucess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorValidForm);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(user);
        }

        //
        // GET: /Admin/User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/User/Delete/5

       [HttpDelete]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
