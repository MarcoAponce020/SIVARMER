using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.DTOs.Portfolio;
using Riesgos.Simefin.Application.Helpers;
using Riesgos.Simefin.Application.Interfaces.Portfolio;
using Riesgos.Simefin.Domain.Entities;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Riesgos.Simefin.Application.UseCases
{

    /// <summary>
    /// Clase que implementa los métodos de lógica de negocios para Portafolios
    /// </summary>
    public class PortfolioUseCase : IPortfolioUseCase
    {

        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IConfiguration _configuration;
        private const int rowDate = 2;

        private readonly string uploadServerFileUrlPath;
        private readonly string uploadServerFolderSource;
        private readonly string downloadServerFileUrlPath;
        private readonly string downloadServerFolderDestination;
        private readonly string fileName;
        private readonly string contentType;
        private readonly string[] originList = ["endpoint", "server"];
        private string procedencia = string.Empty;


        public PortfolioUseCase(IPortfolioRepository portfolioRepository, IConfiguration configuration)
        {
            _portfolioRepository = portfolioRepository;
            _configuration = configuration;
            //Variables de configuración - FileUpload
            uploadServerFileUrlPath = _configuration.GetSection("FileUpload").GetSection("ServerFileUrlPath").Value!;
            uploadServerFolderSource = _configuration.GetSection("FileUpload").GetSection("ServerFolderSource").Value!;
            fileName = _configuration.GetSection("FileUpload").GetSection("FileName").Value!;
            contentType = _configuration.GetSection("FileUpload").GetSection("ContentTypeFile").Value!;
            //Variables de configuración - FileUpload
            downloadServerFileUrlPath = _configuration.GetSection("FileDownload").GetSection("ServerFileUrlPath").Value!;
            downloadServerFolderDestination = _configuration.GetSection("FileDownload").GetSection("ServerFolderDestination").Value!;
        }

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
                ////Portafolio model = _mapper.Map<Portafolio>(request);
                Portafolio model = new Portafolio
                {
                    F_Posicion = request.F_Posicion,
                    ListaDatos = request.ListaDatos,
                    NombrePortafolio = request!.NombrePortafolio,
                    SubPortafolioId = request!.SubPortafolioId
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
                if (!portfolioList!.Any())
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

                ////Portafolio model = _mapper.Map<Portafolio>(request);
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
            //Validar la procedencia del archivo
            var isValidFile = this.ValidateFileExist(ref file);
            if (!isValidFile.IsExitoso) 
            {
                return isValidFile;
            }

            ResponseDTO response = new ResponseDTO();

            string resultReader = string.Empty;
            string worksheetsName = "Hoja1";
            var format = new ExcelTextFormat();
            format.Delimiter = ',';
            format.TextQualifier = '"';
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string? fechaPosicionPivote = string.Empty;
            List<Portafolio> portafoliosList = new List<Portafolio>()!;

            Dictionary<int, string?> dictionaryDates = new Dictionary<int, string?>()!;
            Dictionary<int, string?> dictionaryData = new Dictionary<int, string?>()!;

            try
            {
                //Obteniendo el catálogo de SubPortafolios
                var subPortafolioList = await this.GetSubPortfolios();
                if (!subPortafolioList.Any())
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
                    resultReader = await reader.ReadToEndAsync();
                    worksheet = package?.Workbook.Worksheets.Add(worksheetsName);
                    worksheet = package?.Workbook.Worksheets[0];
                    worksheet!.Cells["A1"].LoadFromText(resultReader, format);

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
                                    ////newPortafolio.SubPortafolio = subPortfolio;
                                    newPortafolio.SubPortafolioId = this.GetSubPortfolioValue(subPortafolioList, subPortfolio);
                                    //Dado el caso de que el SubPortafolioId no existe en BD
                                    if (newPortafolio.SubPortafolioId == 0)
                                    {
                                        response.ErrorMessages.Add($"Configuración errónea en archivo origen. {Environment.NewLine} El valor de SubPortafolio no existe en BD.. Fila ({row}, {col}) => '{subPortfolio}'.");
                                    }
                                }

                                if (col > 1 && subPortafolioList.Any(x => x.SubPortafolioId == newPortafolio.SubPortafolioId))
                                {
                                    dictionaryData.Add(col, currentCellValue);
                                }
                            }
                        } //for col

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
                    } //for row

                    var result = await _portfolioRepository.SavePortfoliosList(portafoliosList);
                    response.ErrorMessages.AddRange(result);
                } //package

                //Guardando archivo en Base64
                if (this.procedencia == originList[1]!) //Servidor
                {
                    //Tomando como fuente el archivo en el server
                    var saved = await this.SaveBackUpServer();
                    if (!saved.IsExitoso)
                    {
                        response.ErrorMessages.Add(saved.Mensaje!);
                    }
                }
                else {
                    //Tomando como fuente el StreamReader del file enviado en el Endpoint
                    var saved = await this.SaveBackUpEndPoint(file);
                    if (!saved.IsExitoso)
                    {
                        response.ErrorMessages.Add(saved.Mensaje!);
                    }
                }

                //Moviendo el archivo
                if (this.procedencia == originList[1]!) //Servidor
                {
                    var moved = await this.MoveFileToServerFolder();
                    if (!moved.IsExitoso)
                    {
                        response.ErrorMessages.Add(moved.Mensaje!);
                    }
                }

                response.IsExitoso = true;
                response.Mensaje = "Archivo procesado con éxito.";
                response.Resultado = true;
                response.StatusCode = HttpStatusCode.OK;
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

        /// <summary>
        /// Verificar la procedencia del archivo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private ResponseDTO ValidateFileExist(ref IFormFile file)
        {
            ResponseDTO response = new ResponseDTO { IsExitoso = false };
            this.procedencia = originList[0]!; //Endpoint

            //Si "file" viene vacío, buscar si viene en el Endpoint
            if (file != null && file!.Length > 0)
            {
                response.IsExitoso = true;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }

            if (file == null || file.Length == 0)
            {
                //Buscar en ruta del servidor
                string pathServerFolderComplete = @$"{this.uploadServerFileUrlPath}{this.uploadServerFolderSource}{this.fileName}";

                //Determina si existe el archivo
                if (File.Exists(pathServerFolderComplete))
                {
                    if (pathServerFolderComplete.ToLower().EndsWith(".csv"))
                    {
                        //Verificando sea el content-type requerido
                        var provider = new FileExtensionContentTypeProvider();
                        string tmpContentType = string.Empty;
                        if (!provider.TryGetContentType(fileName!, out tmpContentType!))
                        {
                            tmpContentType = "none";
                        }
                        if (contentType != tmpContentType)
                        {
                            response.Mensaje = "El contenido o formato (content-type) del archivo no es el esperado.";
                            response.StatusCode = HttpStatusCode.BadRequest;
                            return response;
                        }

                        //Obteniendo archivo de servidor
                        string name = Path.GetFileNameWithoutExtension(pathServerFolderComplete);
                        string fileContent = File.ReadAllText(pathServerFolderComplete);
                        byte[] byteArray = Encoding.UTF8.GetBytes(fileContent);
                        var memoryStream = new MemoryStream(byteArray);

                        IFormFile archivoForm = new FormFile(memoryStream, 0, byteArray.Length, name, fileName!);
                        file = archivoForm;
                        response.IsExitoso = true;
                        response.StatusCode = HttpStatusCode.OK;
                        this.procedencia = originList[1]!; //Servidor
                    }
                    else
                    {
                        response.Mensaje = "El archivo " + pathServerFolderComplete + " no es un .csv.";
                        response.StatusCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.Mensaje = "El archivo " + pathServerFolderComplete + " no existe.";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            
            //Validando en caso de que ni por Endpoint o Carpeta de servidor haya un archivo
            if (file == null || file.Length == 0)
            {
                response.Mensaje = "No se proporcionó un archivo.";
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        /// <summary>
        /// Mover archivo a carpeta de servidor
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseDTO> MoveFileToServerFolder()
        {
            ResponseDTO response = new ResponseDTO { IsExitoso = false };

            var date = DateTime.Now.ToString("s").Replace(":", ".");
            string name = Path.GetFileNameWithoutExtension(this.fileName!);
            string extension = Path.GetExtension(fileName!);

            string sourcePath = @$"{this.uploadServerFileUrlPath}{this.uploadServerFolderSource}{this.fileName}";
            string destinationPath = @$"{this.downloadServerFileUrlPath}{this.downloadServerFolderDestination}{name}-[{date}]{extension}";

            try
            {
                //Determina si existe el archivo
                if (System.IO.File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destinationPath);
                    response.IsExitoso = true;
                }
                else
                {
                    response.Mensaje = "El archivo " + sourcePath + " no existe.";
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ExceptionMessageHelper.GetExceptionMessage(ex);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return await Task.FromResult(response);
        }

        /// <summary>
        /// Guardando el fichero de excel en BD con formato Base64
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseDTO> SaveBackUpServer()
        {
            ResponseDTO response = new ResponseDTO { IsExitoso = false };

            var date = DateTime.Now.ToString("s").Replace(":", ".");
            string name = Path.GetFileNameWithoutExtension(this.fileName!);
            string extension = Path.GetExtension(this.fileName!);

            string sourcePath = @$"{this.uploadServerFileUrlPath}{this.uploadServerFolderSource}{this.fileName}";

            try
            {
                //Determina si existe el archivo
                if (File.Exists(sourcePath))
                {
                    byte[] bytes = await File.ReadAllBytesAsync(sourcePath);
                    ////string base64String = Convert.ToBase64String(bytes);
                    ////byte[] fileBytes = Convert.FromBase64String(base64String);

                    string originalName = fileName!;
                    string compoundName = $"{name}-[{date}]{extension}";

                    var result = await _portfolioRepository.SaveFileBase64(originalName, compoundName, bytes, "SERVER");

                    response.IsExitoso = result;
                }
                else
                {
                    response.Mensaje = "El archivo " + sourcePath + " no existe.";
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ExceptionMessageHelper.GetExceptionMessage(ex);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return response;
        }

        /// <summary>
        /// Guardar archivo CSV proviente del llamado al Endpoint
        /// </summary>
        /// <param name="file">Archivo adjunto</param>
        /// <returns></returns>
        private async Task<ResponseDTO> SaveBackUpEndPoint(IFormFile file)
        {
            ResponseDTO response = new ResponseDTO { IsExitoso = false };

            try
            {
                string originalName = file.FileName;
                var date = DateTime.Now.ToString("s").Replace(":", ".");
                string name = Path.GetFileNameWithoutExtension(originalName);
                string extension = Path.GetExtension(originalName);
                string compoundName = $"{name}-[{date}]{extension}";

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] bytes = memoryStream.ToArray();
                    ////string base64 = Convert.ToBase64String(bytes);

                    //// Guardar en la base de datos (ejemplo con Oracle)
                    //// using (OracleConnection connection = new OracleConnection(connectionString))
                    //// {
                    ////     string sql = "INSERT INTO tabla (nombre_archivo, tipo_contenido, datos_base64) VALUES (:nombre, :tipo, :datos)";
                    ////     OracleCommand command = new OracleCommand(sql, connection);
                    ////     command.Parameters.Add(":nombre", OracleDbType.Varchar2).Value = fileName;
                    ////     command.Parameters.Add(":tipo", OracleDbType.Varchar2).Value = contentType;
                    ////     command.Parameters.Add(":datos", OracleDbType.Varchar2).Value = base64Data;
                    ////
                    ////     connection.Open();
                    ////     command.ExecuteNonQuery();
                    //// }
                    var result = await _portfolioRepository.SaveFileBase64(originalName, compoundName, bytes, "ENDPOINT");
                    response.IsExitoso = result;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ExceptionMessageHelper.GetExceptionMessage(ex);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return response;
        }

        #endregion


        /// <summary>
        /// Obtener archivo almacenado en BD en formato Base64
        /// </summary>
        /// <param name="fileDate">Fecha de almacenamiento</param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetFileCSV(string fileDate)
        {
            ResponseDTO response = new ResponseDTO { IsExitoso = false };

            try
            {
                response = await _portfolioRepository.ReadOracleBLOB(fileDate);
            }
            catch (Exception ex)
            {
                response.Mensaje = ExceptionMessageHelper.GetExceptionMessage(ex);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return response;
        }

    }

}
