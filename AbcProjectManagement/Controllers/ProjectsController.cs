using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AbcProjectManagement.Data;
using AbcProjectManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

namespace AbcProjectManagement.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly UserManager<IdentityUser> userManager;

        public ProjectsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectModel.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectsModel = await _context.ProjectModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectsModel == null)
            {
                return NotFound();
            }

            return View(projectsModel);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectName,Status,Team,Progress")] ProjectsModel projectsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectsModel);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectsModel = await _context.ProjectModel.FindAsync(id);
            if (projectsModel == null)
            {
                return NotFound();
            }
            return View(projectsModel);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectName,Status,Team,Progress")] ProjectsModel projectsModel)
        {
            if (id != projectsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsModelExists(projectsModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectsModel);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectsModel = await _context.ProjectModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectsModel == null)
            {
                return NotFound();
            }

            return View(projectsModel);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectsModel = await _context.ProjectModel.FindAsync(id);
            _context.ProjectModel.Remove(projectsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectsModelExists(int id)
        {
            return _context.ProjectModel.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> EditProjectTeam(int id)
        {
            ViewBag.Id = id;
            var projectsModel = await _context.ProjectModel.FindAsync(id);

            if (projectsModel == null)
            {
                ViewBag.ErrorMessage = $"Project with Id = {id} cannot be found";
                return View("NotFound");
            }
            
            var model = new List<EditProjectTeamViewModel>();
            foreach (var user in userManager.Users)
            {
                var EditProjectTeamViewModel = new EditProjectTeamViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (projectsModel.Team == null)
                {
                    EditProjectTeamViewModel.IsSelected = false;
                } else if (projectsModel.Team.Contains(user.UserName))
                {
                    EditProjectTeamViewModel.IsSelected = true;
                }
                else
                {
                    EditProjectTeamViewModel.IsSelected = false;
                }

                model.Add(EditProjectTeamViewModel);
            }
            return View(model);
            }



        [HttpPost]
        public async Task<IActionResult> EditProjectTeam(List<EditProjectTeamViewModel> model, int id)
        {
            var projectsModel = await _context.ProjectModel.FindAsync(id);

            if (projectsModel == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            string result = null;
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                //result = null;

                if (model[i].IsSelected)
                {
                    result = result + model[i].UserName + "; ";
                }
                else
                {
                    continue;
                }

            }
            projectsModel.Team = result;
            _context.Update(projectsModel);
            await _context.SaveChangesAsync();


            //IdentityResult result = null;

            //if (model[i].IsSelected && !await _context.ProjectModel.ContainsAsync(user.UserName))
            //{
            //    result = await userManager.AddToRoleAsync(user, role.Name);
            //}
            //else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
            //{
            //    result = await userManager.RemoveFromRoleAsync(user, role.Name);
            //}
            //else
            //{
            //    continue;
            //}

            //if (result.Succeeded)
            //{
            //    if (i < (model.Count - 1))
            //        continue;
            //    else
            //        return RedirectToAction("Edit", new { Id = roleId });
            //}


            return RedirectToAction("Edit", new { Id = id });
        }

    }














}

