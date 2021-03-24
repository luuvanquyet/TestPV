using Model;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TestPV.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var dao = new UserDao();
            var model = dao.ListAll();
            return View(model);
        }

        public ActionResult IndexSort()
        {
            var dao = new UserDao();
            var model = dao.ListAllBySort();
            return View(model);
        }
        public ActionResult IndexSortByID()
        {
            var dao = new UserDao();
            var model = dao.ListAllByID();
            return View(model);
        }
        public ActionResult IndexSortByGrID()
        {
            var dao = new UserDao();
            var model = dao.ListAllGrID();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if(user.Name == null|| user.GrID == null)
                {
                    ModelState.AddModelError("", "Invite enter user name and grid! ");
                    return View("Create");
                }
                else
                {
                    int id = dao.Insert(user);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Fail! Not can create user!");
                    }
                }

            }
            return View("Create");
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().GetById(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToRoute(new
                    {
                        controller = "User",
                        action = "Index"
                    });
                }
                else
                {
                    ModelState.AddModelError("", "Update faild!");
                    return RedirectToAction("Edit");
                }

            }
            return RedirectToAction("Index", "User", "");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index", "User");
        }

    }
}