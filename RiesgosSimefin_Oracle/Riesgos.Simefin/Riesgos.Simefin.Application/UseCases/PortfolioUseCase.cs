using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.DTOs.Portfolio;
using Riesgos.Simefin.Application.Helpers;
using Riesgos.Simefin.Application.Interfaces.Portfolio;
using Riesgos.Simefin.Domain.Entities;
using System.Net;
using System.Text.Json;

namespace Riesgos.Simefin.Application.UseCases
{

    /// <summary>
    /// Clase que implementa los métodos de lógica de negocios para Portafolios
    /// </summary>
    public class PortfolioUseCase : IPortfolioUseCase
    {

        private readonly IPortfolioRepository _portfolioRepository;
        private const int rowDate = 2;

        public PortfolioUseCase(IPortfolioRepository portfolioRepository) => _portfolioRepository = portfolioRepository;

        /// <summary>
        /// Crear nuevo Portafolio
        /// </summary>
        /// <param name="request">Datos del Portafolio</param>
        /// <returns></returns>
        public async Task<ResponseDTO> CreatePortfolio(PortafolioCreateDTO request)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                //Portafolio model = _mapper.Map<Portafolio>(request);
                Portafolio model = new Portafolio
                {
                    F_Posicion = request.F_Posicion,
                    ListaDatos = request.ListaDatos,
                    NombrePortafolio = request?.NombrePortafolio,
                    SubPortafolioId = request.SubPortafolioId
                };

                response.Resultado = await _portfolioRepository.AddAsync(model);
                response.IsExitoso = true;
                response.Mensaje = "Registro creado con éxito.";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ExceptionMessageHelper.GetExceptionMessage(ex));
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        /// <summary>
        /// Eliminar un Portafolio
        /// </summary>
        /// <param name="id">Identificador de Portafolio</param>
        /// <returns></returns>
        public async Task<ResponseDTO> DeletePortfolio(int id)
        {
            ResponseDTO response = new ResponseDTO();

            if (id == 0)
            {
                response.IsExitoso = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                var deletePortfolio = await this.GetPortfolioById(id);
                if (deletePortfolio == null || (deletePortfolio != null && !deletePortfolio.IsExitoso))
                {
                    return deletePortfolio!;
                }

                response.Resultado = await _portfolioRepository.DeleteAsync(id);
                response.IsExitoso = true;
                response.Mensaje = "Registro eliminado con éxito.";
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ExceptionMessageHelper.GetExceptionMessage(ex));
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        /// <summary>
        /// Obtener todos los Portafolios
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDTO> GetAllPortfolio()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                response.Resultado = await _portfolioRepository.GetAllAsync();
                response.IsExitoso = true;
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ExceptionMessageHelper.GetExceptionMessage(ex));
            }

            return response;
        }

        /// <summary>
        /// Obtener portafolio por una Fecha
        /// </summary>
        /// <param name="positionDate">Fecha de Posición</param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetPortfolioByDate(string positionDate)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                //Construir lista de parámetros por los cuales filtrar.
                //El nombre del parámetro debe ser el nombre de la columna en BD.
                List<object> filters = new List<object>() { 
                    new { 
                        F_Posicion = positionDate
                    } 
                };
                IEnumerable<Portafolio> portfolioList = await _portfolioRepository.GetAllAsync(filters);
                if (!portfolioList!.Any() && portfolioList?.Count() == 0)
                {
                    response.IsExitoso = false;
                    response.ErrorMessages.Add("No existe Información para esta Fecha");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                response.Resultado = portfolioList;
                response.IsExitoso = response.Resultado != null;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ExceptionMessageHelper.GetExceptionMessage(ex));
            }

            return response;
        }

        /// <summary>
        /// Obtener portafolio por Identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetPortfolioById(int id)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                response.Resultado = await _portfolioRepository.GetByIdAsync(id);
                response.IsExitoso = response.Resultado != null;
                response.StatusCode = HttpStatusCode.OK;
                if (response.Resultado == null)
                {
                    response.Mensaje = "Registro no encontrado.";
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ExceptionMessageHelper.GetExceptionMessage(ex));
            }

            return response;
        }

        /// <summary>
        /// Actualización de un portafolio
        /// </summary>
        /// <param name="portafolioId">Identificador del Portafolio</param>
        /// <param name="request">Contenedor con información de Portafolio</param>
        /// <returns></returns>
        public async Task<ResponseDTO> UpdatePortfolio(int portafolioId, PortafolioUpdateDTO request)
        {
            ResponseDTO response = new ResponseDTO();

            if (portafolioId == 0 || (request == null || string.IsNullOrEmpty(request.ListaDatos)))
            {
                response.IsExitoso = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                var updatePortfolio = await _portfolioRepository.GetByIdAsync(portafolioId);
                if (updatePortfolio == null || updatePortfolio.IdPortafolio <= 0)
                {
                    response.IsExitoso = false;
                    response.Mensaje = "Registro no encontrado.";
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                //Portafolio model = _mapper.Map<Portafolio>(request);
                Portafolio model = new Portafolio
                {
                    IdPortafolio = updatePortfolio.IdPortafolio,
                    ListaDatos = request.ListaDatos,
                    No_Envio = updatePortfolio.No_Envio,
                };

                response.Resultado = await _portfolioRepository.UpdateAsync(model);
                response.IsExitoso = true;
                response.Mensaje = "Registro actualizado con éxito.";
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ExceptionMessageHelper.GetExceptionMessage(ex));
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }


        #region Carga de Excel ...

        /// <summary>
        /// Carga de archivo de excel con registros de portafolios
        /// </summary>
        /// <param name="file">Archivo de Excel</param>
        /// <returns></returns>
        public async Task<ResponseDTO> ExcelLoad(IFormFile file)
        {
            ResponseDTO response = new ResponseDTO();

            var resultReader = string.Empty;
            string worksheetsName = "Hoja1";
            var format = new ExcelTextFormat();
            format.Delimiter = ',';
            format.TextQualifier = '"';
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string? fechaPosicionPivote = string.Empty;
            List<Portafolio> portafoliosList = new List<Portafolio>()!;

            Dictionary<int, string?> dictionaryDates = new Dictionary<int, string?>()!;
            Dictionary<int, string?> dictionaryData = new Dictionary<int, string?>()!;
            List<string> listaDatos = new List<string>()!;

            try
            {
                //Obteniendo el catálogo de SubPortafolios
                var subPortafolioList = await this.GetSubPortfolios();
                if (!subPortafolioList.Any() && subPortafolioList.Count() == 0)
                {
                    response.Mensaje = "Faltan datos para procesar -Catálogo de SubPortafolios-.";
                    response.IsExitoso = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                ExcelWorksheet? worksheet = null;
                using (var reader = new System.IO.StreamReader(file.OpenReadStream()))
                using (ExcelPackage package = new ExcelPackage())
                {
                    resultReader = reader.ReadToEnd();
                    worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                    worksheet = package.Workbook.Worksheets[0];
                    worksheet.Cells["A1"].LoadFromText(resultReader, format);

                    //Validando la fecha pivote
                    fechaPosicionPivote = worksheet.Cells[1, 1].Value?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(fechaPosicionPivote))
                    {
                        response.Mensaje = "Faltan datos para procesar -fecha de posición inicial-.";
                        response.IsExitoso = false;
                        response.StatusCode = HttpStatusCode.BadRequest;
                        return response;
                    }

                    //Obteniendo el listado de fechas
                    dictionaryDates = this.GetDateList(worksheet);
                    if (!dictionaryDates.Any() && dictionaryDates.Count == 0)
                    {
                        response.Mensaje = "Faltan datos para procesar -fechas de portafolios-.";
                        response.IsExitoso = false;
                        response.StatusCode = HttpStatusCode.BadRequest;
                        return response;
                    }

                    Portafolio newPortafolio = new Portafolio()!;
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count

                    //Partiendo de la fila 3, donde se encuentra el cuerpo de los portafolios
                    for (int row = 3; row <= rowCount; row++)
                    {
                        dictionaryData = new Dictionary<int, string?>()!;
                        newPortafolio = new Portafolio
                        {
                            F_Posicion = fechaPosicionPivote,
                            NombrePortafolio = "TOTAL",
                            No_Envio = 1,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        for (int col = 1; col <= colCount; col++)
                        {
                            string? currentCellValue = worksheet.Cells[row, col].Value?.ToString()?.Trim();
                            if (!string.IsNullOrEmpty(currentCellValue))
                            {
                                //Subportafolios
                                if (col == 1)
                                {
                                    string? subPortfolio = currentCellValue.ToUpper();
                                    //newPortafolio.SubPortafolio = subPortfolio;
                                    newPortafolio.SubPortafolioId = this.GetSubPortfolioValue(subPortafolioList, subPortfolio);
                                    //Dado el caso de que el SubPortafolioId no existe en BD
                                    if (newPortafolio.SubPortafolioId == 0)
                                    {
                                        response.ErrorMessages.Add($"Configuración errónea en archivo origen. {Environment.NewLine} El valor de SubPortafolio no existe en BD.. Fila ({row}, {col}) => '{subPortfolio}'.");
                                    }
                                }

                                if (col > 1)
                                {
                                    if (subPortafolioList.Any(x => x.SubPortafolioId == newPortafolio.SubPortafolioId))
                                    {
                                        dictionaryData.Add(col, currentCellValue);
                                    }
                                }
                            }
                        } //col

                        if (newPortafolio != null && newPortafolio.SubPortafolioId > 0)
                        {
                            //Comparando fechas vs datos. En cada diccionario se agregó como Key la columna.
                            //Entonces se compara que las claves de cada diccionario existan en ambos. Si hay diferencia, abortar
                            bool isKeysTheSame = dictionaryDates.Count == dictionaryData.Count && !dictionaryDates.Keys.Except(dictionaryData.Keys).Any();
                            if (!isKeysTheSame)
                            {
                                response.Mensaje = "Archivo no procesado por incoherencia en datos.";
                                response.ErrorMessages.Add($"Fila ({row}). La lista de fechas no coincide con la lista de datos.");
                                response.IsExitoso = false;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                return response;
                            }

                            var datos = (from f in dictionaryDates
                                         join d in dictionaryData on f.Key equals d.Key
                                         select new { Fecha = f.Value, Valor = d.Value }).ToList();

                            if (datos != null)
                            {
                                newPortafolio.ListaDatos = JsonSerializer.Serialize(datos);
                            }

                            //Agregando el nuevo porfatolio a la lista
                            portafoliosList.Add(newPortafolio);
                        }
                    } //row

                    var jsonString = JsonSerializer.Serialize(portafoliosList);

                    var result = await _portfolioRepository.SavePortfoliosList(portafoliosList);
                    response.ErrorMessages.AddRange(result);
                }
            }
            catch (KeyNotFoundException ke)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add($"Datos corruptos. {ExceptionMessageHelper.GetExceptionMessage(ke)}");
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Resultado = false;
                return response;
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ExceptionMessageHelper.GetExceptionMessage(ex));
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Resultado = false;
                return response;
            }

            response.Resultado = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Mensaje = "Archivo procesado con éxito.";
            return response;
        }

        /// <summary>
        /// Obtener el valor de la segunda columna del archivo (F_Posicion)
        /// </summary>
        /// <param name="worksheet">Hoja en el archivo de excel a examinar</param>
        /// <returns></returns>
        private Dictionary<int, string?> GetDateList(ExcelWorksheet worksheet)
        {
            Dictionary<int, string?> dateList = new Dictionary<int, string?>();
            int colCount = worksheet.Dimension.End.Column;

            for (int col = 2; col <= colCount; col++)
            {
                string? currentCellValue = worksheet.Cells[rowDate, col].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(currentCellValue))
                {
                    dateList.Add(col, currentCellValue);
                }
            }

            return dateList;
        }

        /// <summary>
        /// Obtener el catálogo de los SubPortafolios
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<SubPortafolios>> GetSubPortfolios()
        {
            var response = await _portfolioRepository.GetAllSubPortfoliosAsync();
            return response;
        }

        /// <summary>
        /// Obtener el identificador del SubPortafolio
        /// </summary>
        /// <param name="subPortfolioList">Catálogo de SubPortafolios</param>
        /// <param name="value">SubPortafolio proveniente del Excel</param>
        /// <returns></returns>
        private int GetSubPortfolioValue(IEnumerable<SubPortafolios> subPortfolioList, string? value)
        {
            int id = 0;

            if (!string.IsNullOrEmpty(value))
            {
                id = subPortfolioList.Where(x => x.Descripcion?.Trim().ToUpper() == value.Trim().ToUpper()).Select(y => y.SubPortafolioId).FirstOrDefault();
            }

            return id;
        }

        #endregion

    }

}
