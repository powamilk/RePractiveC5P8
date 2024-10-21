using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Entities;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace AppView.Controllers
{
    public class DeThisController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _apiBaseUrl = "https://localhost:7202/DeThis";

        public DeThisController()
        {
            _client = new();
        }

        // GET: DeThis
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var deThis = await response.Content.ReadAsStringAsync();
                var deThiList = JsonSerializer.Deserialize<List<DeThi>>(deThis);
                return View(deThiList);
            }
            ModelState.AddModelError(string.Empty, "Không thể tải danh sách Đề Thi.");
            return View(new List<DeThi>());
        }

        // GET: DeThis/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var deThi = await response.Content.ReadAsStringAsync();
                var deThiDetail = JsonSerializer.Deserialize<DeThi>(deThi);
                return View(deThiDetail);
            }

            return NotFound();
        }

        // GET: DeThis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeThis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenDeThi,MonHoc,NgayThi,ThoiGianLamBai,SoLuongCauHoi,Status")] DeThi deThi)
        {
            if (ModelState.IsValid)
            {
                deThi.Id = Guid.NewGuid();
                var jsonContent = JsonSerializer.Serialize(deThi);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(_apiBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Không thể tạo mới Đề thi.");
            }

            return View(deThi);
        }

        // GET: DeThis/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var deThi = await response.Content.ReadAsStringAsync();
                var deThiDetail = JsonSerializer.Deserialize<DeThi>(deThi);
                return View(deThiDetail);
            }

            return NotFound();
        }

        // POST: DeThis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TenDeThi,MonHoc,NgayThi,ThoiGianLamBai,SoLuongCauHoi,Status")] DeThi deThi)
        {
            if (id != deThi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var jsonContent = JsonSerializer.Serialize(deThi);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"{_apiBaseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Không thể cập nhật Đề thi.");
            }

            return View(deThi);
        }

        // GET: DeThis/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var deThi = await response.Content.ReadAsStringAsync();
                var deThiDetail = JsonSerializer.Deserialize<DeThi>(deThi);
                return View(deThiDetail);
            }

            return NotFound();
        }

        // POST: DeThis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _client.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Không thể xóa đề thi.");
            return View();
        }

        private bool DeThiExists(Guid id)
        {
            var response = _client.GetAsync($"{_apiBaseUrl}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
