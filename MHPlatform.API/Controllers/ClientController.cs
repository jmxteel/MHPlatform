using MHPlatform.Service.IService;
using MHPlatform.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace MHPlatform.API.Controllers
{
    [Route("api/Client")]
    [Authorize(Policy = "CanAccessProducts")]
    [ApiController]
    public class ClientController: ControllerBase   
    {
        private readonly IOrderFormService _service;
        const int maxProductPageSize = 100;
        public ClientController(IOrderFormService service)
        {
            this._service = service;
        }

        [HttpGet("{ordrNo}")]
        public async Task<IActionResult> GetClient(string ordrNo)
        {
            try
            {
                var client = await _service.GetClientDetails(ordrNo);
                return Ok(client);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        [HttpGet("AllClients")]
        public async Task<IActionResult> GetAllClients(string? filter, string? q, int pageNumber = 1, int pageSize = 20)
        {

            try
            {
                if (pageSize > maxProductPageSize)
                {
                    pageSize = maxProductPageSize;
                }

                var (clients, paginationMetaData) = await _service.GetAllClientAsync(filter, q, pageNumber, pageSize);
                if (clients == null)
                {
                    return NotFound();
                }

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

                return Ok(clients);

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        private static List<Person> _people = new List<Person>
        {
            new Person { Id = 1, Name = "Name 1", Email = "email1@example.com" },
            new Person { Id = 2, Name = "Name 2", Email = "email2@example.com" },
            new Person { Id = 3, Name = "Name 3", Email = "email3@example.com" },
            new Person { Id = 4, Name = "Name 4", Email = "email4@example.com" },
            new Person { Id = 5, Name = "Name 5", Email = "email5@example.com" },
            new Person { Id = 6, Name = "Name 6", Email = "email6@example.com" },
            new Person { Id = 7, Name = "Name 7", Email = "email7@example.com" },
            new Person { Id = 8, Name = "Name 8", Email = "email8@example.com" },
            new Person { Id = 9, Name = "Name 9", Email = "email9@example.com" },
            new Person { Id = 10, Name = "Name 10", Email = "email10@example.com" },
            new Person { Id = 11, Name = "Name 11", Email = "email11@example.com" },
            new Person { Id = 12, Name = "Name 12", Email = "email12@example.com" },
            new Person { Id = 13, Name = "Name 13", Email = "email13@example.com" },
            new Person { Id = 14, Name = "Name 14", Email = "email14@example.com" },
            new Person { Id = 15, Name = "Name 15", Email = "email15@example.com" },
            new Person { Id = 16, Name = "Name 16", Email = "email16@example.com" },
            new Person { Id = 17, Name = "Name 17", Email = "email17@example.com" },
            new Person { Id = 18, Name = "Name 18", Email = "email18@example.com" },
            new Person { Id = 19, Name = "Name 19", Email = "email19@example.com" },
            new Person { Id = 20, Name = "Name 20", Email = "email20@example.com" },
            new Person { Id = 21, Name = "Name 21", Email = "email21@example.com" },
            new Person { Id = 22, Name = "Name 22", Email = "email22@example.com" },
            new Person { Id = 23, Name = "Name 23", Email = "email23@example.com" },
            new Person { Id = 24, Name = "Name 24", Email = "email24@example.com" },
            new Person { Id = 25, Name = "Name 25", Email = "email25@example.com" },
            new Person { Id = 26, Name = "Name 26", Email = "email26@example.com" },
            new Person { Id = 27, Name = "Name 27", Email = "email27@example.com" },
            new Person { Id = 28, Name = "Name 28", Email = "email28@example.com" },
            new Person { Id = 29, Name = "Name 29", Email = "email29@example.com" },
            new Person { Id = 30, Name = "Name 30", Email = "email30@example.com" },
            new Person { Id = 31, Name = "Name 31", Email = "email21@example.com" },
            new Person { Id = 32, Name = "Name 32", Email = "email22@example.com" },
            new Person { Id = 33, Name = "Name 33", Email = "email23@example.com" },
            new Person { Id = 34, Name = "Name 34", Email = "email24@example.com" },
            new Person { Id = 35, Name = "Name 35", Email = "email25@example.com" },
            new Person { Id = 36, Name = "Name 36", Email = "email26@example.com" },
            new Person { Id = 37, Name = "Name 37", Email = "email27@example.com" },
            new Person { Id = 38, Name = "Name 38", Email = "email28@example.com" },
            new Person { Id = 39, Name = "Name 39", Email = "email29@example.com" },
            new Person { Id = 40, Name = "Name 40", Email = "email30@example.com" },
        };

        // GET api/persons
        [HttpGet]
        public IActionResult GetPagedData([FromQuery] int start = 0, [FromQuery] int end = 10, [FromQuery] string? sortField = "Id", [FromQuery] string? sortOrder = "asc")
        {

            //var rows = _people.Skip(start).Take(end - start).ToList();

            //var response = new PagedResponse
            //{
            //    Rows = rows,
            //    TotalCount = _people.Count
            //};

            //return Ok(response);




            // Default to 'asc' if sortOrder is missing or invalid
            if (sortOrder != "asc" && sortOrder != "desc")
            {
                sortOrder = "asc"; // Default to ascending order if invalid
            }

            // Default sorting field is 'Id' if sortField is missing or invalid
            if (string.IsNullOrWhiteSpace(sortField))
            {
                sortField = "Id"; // Default to sorting by 'Id'
            }

            // Apply pagination and sorting on the list of people
            var query = _people.AsQueryable();  // Use IQueryable to allow dynamic sorting

            // Apply sorting logic based on the sortField and sortOrder
            switch (sortField.ToLower())
            {
                case "id":
                    query = sortOrder == "asc" ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                    break;
                case "name":
                    query = sortOrder == "asc" ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;
                case "email":
                    query = sortOrder == "asc" ? query.OrderBy(p => p.Email) : query.OrderByDescending(p => p.Email);
                    break;
                default:
                    // If an invalid sortField is provided, default to sorting by 'Id'
                    query = sortOrder == "asc" ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                    break;
            }

            // Apply pagination using Skip and Take
            var rows = query.Skip(start).Take(end).ToList();

            // Create the response object
            var response = new PagedResponse
            {
                Rows = rows,
                TotalCount = _people.Count
            };

            return Ok(response);

        }
    }
}
