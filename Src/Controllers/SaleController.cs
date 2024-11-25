using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using TallerBackendIDWM.Src.DTOs.Shopping;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Controllers
{
    /// <summary>
    /// Controlador para gestionar las ventas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        /// <summary>
        /// Constructor del controlador de ventas.
        /// </summary>
        /// <param name="saleService">Servicio de ventas.</param>
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Obtiene todas las ventas registradas.
        /// </summary>
        /// <returns>Lista de ventas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetSalesAsync();
            return Ok(sales);
        }

        /// <summary>
        /// Obtiene los detalles de una venta específica por su ID.
        /// </summary>
        /// <param name="id">ID de la venta.</param>
        /// <returns>Detalle de la venta correspondiente al ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound(new { message = "Venta no encontrada." });
            }
            return Ok(sale);
        }
    }
}