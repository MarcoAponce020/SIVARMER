using System;
using System.Xml.Serialization;
using System.Transactions;
using System.ComponentModel;
using System.Xml.XPath;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace DAO
{
    public class DAORiesgos
    {
        private const string Value = "FechaValor";


        #region Métodos para guardar ....

        /// <summary>
        /// Reporte # 1
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public Respuesta guardarValuacionReportos(List<ValuacionReportos> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarValuacionReportos(reporte[0].FechaValuacion, conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (ValuacionReportos item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FECHAVALUACION", item.FechaValuacion);
                        cmd.Parameters.Add("EMISION", item.Emision);
                        cmd.Parameters.Add("TIPO", item.Tipo);
                        cmd.Parameters.Add("FECHA", item.Fecha);
                        cmd.Parameters.Add("NUMCONTRATO", item.NumContrato);
                        cmd.Parameters.Add("NUMREFERENCIA", item.NumReferencia);
                        cmd.Parameters.Add("MOVIMIENTOOPER", item.MovimientoOper);
                        cmd.Parameters.Add("PARTE", item.Parte);
                        cmd.Parameters.Add("TITULOS", item.Titulos);
                        cmd.Parameters.Add("PRECIOOPERACION", item.PrecioOperacion);
                        cmd.Parameters.Add("IMPORTEOPERACION", item.ImporteOperacion);
                        cmd.Parameters.Add("IMPORTELIBROS", item.ImporteLibros);
                        cmd.Parameters.Add("DIASTRANSCURRIDOS", item.DiasTranscurridos);
                        cmd.Parameters.Add("DXV", item.DxV);
                        cmd.Parameters.Add("TASAVENCIMIENTO", item.TasaVencimiento);
                        cmd.Parameters.Add("TASADIARIA", item.TasaDiaria);
                        cmd.Parameters.Add("TASACURVA", item.TasaCurva);
                        cmd.Parameters.Add("PREMIO", item.Premio);
                        cmd.Parameters.Add("PRECIOVECTOR", item.PrecioVector);
                        cmd.Parameters.Add("VALORMERCADO", item.ValorMercado);
                        cmd.Parameters.Add("PORTAFOLIO", item.Portafolio);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_VAL_REPORTOS (FECHAVALUACION" +
                                                                                 ",EMISION" +
                                                                                 ",TIPO" +
                                                                                 ",FECHA" +
                                                                                 ",NUMCONTRATO" +
                                                                                 ",NUMREFERENCIA" +
                                                                                 ",MOVIMIENTOOPER" +
                                                                                 ",PARTE" +
                                                                                 ",TITULOS" +
                                                                                 ",PRECIOOPERACION" +
                                                                                 ",IMPORTEOPERACION" +
                                                                                 ",IMPORTELIBROS" +
                                                                                 ",DIASTRANSCURRIDOS" +
                                                                                 ",DXV" +
                                                                                 ",TASAVENCIMIENTO" +
                                                                                 ",TASADIARIA" +
                                                                                 ",TASACURVA" +
                                                                                 ",PREMIO" +
                                                                                 ",PRECIOVECTOR" +
                                                                                 ",VALORMERCADO" +
                                                                                 ",PORTAFOLIO) " +
                                                                          "VALUES (:FECHAVALUACION, :EMISION, :TIPO, :FECHA, :NUMCONTRATO, :NUMREFERENCIA, :MOVIMIENTOOPER, :PARTE, :TITULOS, :PRECIOOPERACION, :IMPORTEOPERACION, :IMPORTELIBROS, :DIASTRANSCURRIDOS," +
                                                                                  ":DXV, :TASAVENCIMIENTO, :TASADIARIA, :TASACURVA, :PREMIO, :PRECIOVECTOR, :VALORMERCADO, :PORTAFOLIO)";

                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje += "Hubo un error al borrar los registros. " + conexion.ConnectionString.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        /// <summary>
        /// Reporte # 2
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public Respuesta guardarTenenciaTitulos(List<TenenciaTitulos> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarTenenciaTitulos(reporte[0].FechaValuacion, conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (TenenciaTitulos item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FECHAVALUACION", item.FechaValuacion);
                        cmd.Parameters.Add("EMISION", item.Emision);
                        cmd.Parameters.Add("TITULOS", item.Titulos);
                        cmd.Parameters.Add("ImporteSucio", item.ImporteSucio);
                        cmd.Parameters.Add("TIPO", item.Tipo);
                        cmd.Parameters.Add("Descripcion", item.Descripcion);
                        cmd.Parameters.Add("PORTAFOLIO", item.Portafolio);
                        cmd.Parameters.Add("NumOperacion", item.NumOperacion);
                        cmd.Parameters.Add("InteresDevengado", item.InteresDevengado);
                        cmd.Parameters.Add("TitulosGarantia", item.TitulosGarantia);
                        cmd.Parameters.Add("Intercupon", item.Intercupon);
                        cmd.Parameters.Add("DXVencer", item.DXVencer);
                        cmd.Parameters.Add("DXVRC0103", item.DXVRC0103);
                        cmd.Parameters.Add("PrecioSucio", item.PrecioSucio);
                        cmd.Parameters.Add("PrecioLimpio", item.PrecioLimpio);
                        cmd.Parameters.Add("ImporteLimpio", item.ImporteLimpio);
                        cmd.Parameters.Add("ImporteInteres", item.ImporteInteres);
                        cmd.Parameters.Add("PrecioMercado", item.PrecioMercado);
                        cmd.Parameters.Add("ImporteMercado", item.ImporteMercado);
                        cmd.Parameters.Add("DXVRC02", item.DXVRC02);
                        cmd.Parameters.Add("PlusMinus", item.PlusMinus);
                        cmd.Parameters.Add("TasaCosto", item.TasaCosto);
                        cmd.Parameters.Add("Parte", item.Parte);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_TENENCIA_TITULOS (FECHAVALUACION" +
                                                                                                   ",EMISION" +
                                                                                                   ",TITULOS" +
                                                                                                   ",ImporteSucio" +
                                                                                                   ",TIPO" +
                                                                                                   ",Descripcion" +
                                                                                                   ",PORTAFOLIO" +
                                                                                                   ",NumOperacion" +
                                                                                                   ",InteresDevengado" +
                                                                                                   ",TitulosGarantia" +
                                                                                                   ",Intercupon" +
                                                                                                   ",DXVencer" +
                                                                                                   ",DXVRC0103" +
                                                                                                   ",PrecioSucio" +
                                                                                                   ",PrecioLimpio" +
                                                                                                   ",ImporteLimpio" +
                                                                                                   ",ImporteInteres" +
                                                                                                   ",PrecioMercado" +
                                                                                                   ",ImporteMercado" +
                                                                                                   ",DXVRC02" +
                                                                                                   ",PlusMinus" +
                                                                                                   ",TasaCosto" +
                                                                                                   ",Parte) " +
                                                                                            "VALUES (:FECHAVALUACION,:EMISION,:TITULOS,:ImporteSucio,:TIPO,:Descripcion,:PORTAFOLIO,:NumOperacion,:InteresDevengado,:TitulosGarantia,:Intercupon,:DXVencer,:DXVRC0103,:PrecioSucio,:PrecioLimpio,:ImporteLimpio,:ImporteInteres,:PrecioMercado,:ImporteMercado,:DXVRC02,:PlusMinus,:TasaCosto,:Parte)";

                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        public Respuesta guardarComprasMesaDinero(List<ComprasMesaDinero> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarComprasMesaDinero(Convert.ToInt32(reporte[0].FechaValor), conexion);

            conexion.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;


            try
            {
                if (borro)
                {
                    //conexion.Open();
                    int aff = 0;
                    foreach (ComprasMesaDinero item in reporte)
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FechaValor", item.FechaValor);
                        cmd.Parameters.Add("Moneda", item.Moneda);
                        cmd.Parameters.Add("NumOperacion", item.NumOperacion);
                        cmd.Parameters.Add("Contraparte", item.Contraparte);
                        cmd.Parameters.Add("Papel", item.Papel);
                        cmd.Parameters.Add("Emision", item.Emision);
                        cmd.Parameters.Add("Posicion", item.Posicion);
                        cmd.Parameters.Add("FV", item.FV);
                        cmd.Parameters.Add("NumTitulos", item.NumTitulos);
                        cmd.Parameters.Add("PrecioSucio", item.PrecioSucio);
                        cmd.Parameters.Add("TasaRend", item.TasaRend);
                        cmd.Parameters.Add("TasaDiaria", item.TasaDiaria);
                        cmd.Parameters.Add("Plazo", item.Plazo);
                        cmd.Parameters.Add("ImporteReal", item.ImporteReal);
                        cmd.Parameters.Add("Portafolio", item.Portafolio);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_COMPRAS_MD (FechaValor" +
                                                                                            ",Moneda" +
                                                                                            ",NumOperacion" +
                                                                                            ",Contraparte" +
                                                                                            ",Papel" +
                                                                                            ",Emision" +
                                                                                            ",Posicion" +
                                                                                            ",FV" +
                                                                                            ",NumTitulos" +
                                                                                            ",PrecioSucio" +
                                                                                            ",TasaRend" +
                                                                                            ",TasaDiaria" +
                                                                                            ",Plazo" +
                                                                                            ",ImporteReal" +
                                                                                            ",PORTAFOLIO) " +
                                                                                            "VALUES (:FechaValor,:Moneda,:NumOperacion,:Contraparte,:Papel,:Emision,:Posicion,:FV,:NumTitulos,:PrecioSucio,:TasaRend,:TasaDiaria,:Plazo,:ImporteReal,:Portafolio)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        public Respuesta guardarComprasTesoreria(List<ComprasTesoreria> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarComprasTesoreria(Convert.ToInt32(reporte[0].FechaValor), conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (ComprasTesoreria item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FechaValor", item.FechaValor);
                        cmd.Parameters.Add("Moneda", item.Moneda);
                        cmd.Parameters.Add("NumOperacion", item.NumOperacion);
                        cmd.Parameters.Add("Contraparte", item.Contraparte);
                        cmd.Parameters.Add("Papel", item.Papel);
                        cmd.Parameters.Add("Emision", item.Emision);
                        cmd.Parameters.Add("Posicion", item.Posicion);
                        cmd.Parameters.Add("FV", item.FV);
                        cmd.Parameters.Add("NumTitulos", item.NumTitulos);
                        cmd.Parameters.Add("PrecioSucio", item.PrecioSucio);
                        cmd.Parameters.Add("TasaRend", item.TasaRend);
                        cmd.Parameters.Add("TasaDiaria", item.TasaDiaria);
                        cmd.Parameters.Add("Plazo", item.Plazo);
                        cmd.Parameters.Add("ImporteReal", item.ImporteReal);
                        cmd.Parameters.Add("Portafolio", item.Portafolio);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_COMPRAS_TESO (FechaValor" +
                                                                                               ",Moneda" +
                                                                                               ",NumOperacion" +
                                                                                               ",Contraparte" +
                                                                                               ",Papel" +
                                                                                               ",Emision" +
                                                                                               ",Posicion" +
                                                                                               ",FV" +
                                                                                               ",NumTitulos" +
                                                                                               ",PrecioSucio" +
                                                                                               ",TasaRend" +
                                                                                               ",TasaDiaria" +
                                                                                               ",Plazo" +
                                                                                               ",ImporteReal" +
                                                                                               ",PORTAFOLIO) " +
                                                                                        "VALUES (:FechaValor,:Moneda,:NumOperacion,:Contraparte,:Papel,:Emision,:Posicion,:FV,:NumTitulos,:PrecioSucio,:TasaRend,:TasaDiaria,:Plazo,:ImporteReal,:Portafolio)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        public Respuesta guardarPosicionPatrimonial(List<PosicionPatrimonial> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarPosicionPatrimonial(Convert.ToInt32(reporte[0].FechaValuacion), conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (PosicionPatrimonial item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FechaValuacion", item.FechaValuacion);
                        cmd.Parameters.Add("FechaInicioOper", item.FechaInicioOper);
                        cmd.Parameters.Add("NumOpera", item.NumOpera);
                        cmd.Parameters.Add("Cliente", item.Cliente);
                        cmd.Parameters.Add("TipoInstrumento", item.TipoInstrumento);
                        cmd.Parameters.Add("NumTitulosPos", item.NumTitulosPos);
                        cmd.Parameters.Add("Monto", item.Monto);
                        cmd.Parameters.Add("Plazo", item.Plazo);
                        cmd.Parameters.Add("DT", item.DT);
                        cmd.Parameters.Add("DXV", item.DXV);
                        cmd.Parameters.Add("Tasa", item.Tasa);
                        cmd.Parameters.Add("PremioDev", item.PremioDev);
                        cmd.Parameters.Add("ImpCIntereses", item.ImpCIntereses);
                        cmd.Parameters.Add("FechaVencimiento", item.FechaVencimiento);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_PATRIMONIAL (FechaValuacion" +
                                                                                              ",FechaInicioOper" +
                                                                                              ",NumOpera" +
                                                                                              ",Cliente" +
                                                                                              ",TipoInstrumento" +
                                                                                              ",NumTitulosPos" +
                                                                                              ",Monto" +
                                                                                              ",Plazo" +
                                                                                              ",DT" +
                                                                                              ",DXV" +
                                                                                              ",Tasa" +
                                                                                              ",PremioDev" +
                                                                                              ",ImpCIntereses" +
                                                                                              ",FechaVencimiento) " +
                                                                                       "VALUES (:FechaValuacion,:FechaInicioOper,:NumOpera,:Cliente,:TipoInstrumento,:NumTitulosPos,:Monto,:Plazo,:DT,:DXV,:Tasa,:PremioDev,:ImpCIntereses,:FechaVencimiento)";

                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        public Respuesta guardarReporteREVAME(List<ReporteREVAME> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarReporteREVAME(Convert.ToInt32(reporte[0].Fecha), conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (ReporteREVAME item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("Fecha", item.Fecha);
                        cmd.Parameters.Add("Portafolio", item.Portafolio);
                        cmd.Parameters.Add("Emision", item.Emision);
                        cmd.Parameters.Add("Titulos", item.Titulos);
                        cmd.Parameters.Add("PrecioLimpio", item.PrecioLimpio);
                        cmd.Parameters.Add("PrecioSucio", item.PrecioSucio);
                        cmd.Parameters.Add("ImporteLimpio", item.ImporteLimpio);
                        cmd.Parameters.Add("ImporteSucio", item.ImporteSucio);
                        cmd.Parameters.Add("PrecioLimpioLib", item.PrecioLimpioLib);
                        cmd.Parameters.Add("PrecioSucioLib", item.PrecioSucioLib);
                        cmd.Parameters.Add("ImporteLimpioLib", item.ImporteLimpioLib);
                        cmd.Parameters.Add("ImporteSucioLib", item.ImporteSucioLib);
                        cmd.Parameters.Add("Plus_Minus", item.Valuacion);
                        cmd.Parameters.Add("Precio_Mercado", item.PrecioMercado);

                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_REVAME (Fecha" +
                                                                            ",Portafolio" +
                                                                            ",Emision" +
                                                                            ",Titulos" +
                                                                            ",PrecioLimpio" +
                                                                            ",PrecioSucio" +
                                                                            ",ImporteLimpio" +
                                                                            ",ImporteSucio" +
                                                                            ",PrecioLimpioLib" +
                                                                            ",PrecioSucioLib" +
                                                                            ",ImporteLimpioLib" +
                                                                            ",ImporteSucioLib" +
                                                                            ",Plus_Minus" +
                                                                            ",Precio_Mercado) " +
                                                                             "VALUES (:Fecha,:Portafolio,:Emision,:Titulos,:PrecioLimpio,:PrecioSucio,:ImporteLimpio,:ImporteSucio,:PrecioLimpioLib,:PrecioSucioLib,:ImporteLimpioLib,:ImporteSucioLib,:Plus_Minus,:Precio_Mercado)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        public Respuesta guardarPosicionCalculoVAR(List<PosicionCalculoVAR> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarPosicionCalculoVAR(Convert.ToInt32(reporte[0].Fecha), conexionRiesgos);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexionRiesgos;

            try
            {
                if (borro)
                {
                    conexionRiesgos.Open();
                    int aff = 0;
                    foreach (PosicionCalculoVAR item in reporte)
                    {
                        string newPortafolio = "null"; //Variable para nombrar el portafolio de CENTURA
                        int newNumPortafolio =0;

                        if (item.NumPortafolio == 1)
                        {
                            newPortafolio = "Titulos a Negociar";
                            newNumPortafolio =0;
                        }

                        if (item.NumPortafolio == 7 & item.TipoPosicion == 2)
                        {
                            newPortafolio = "PI_IFCPI_TV";
                            newNumPortafolio =15;

                        }

                        if (item.NumPortafolio == 8 & item.TipoPosicion == 2)
                        {
                            newPortafolio = "PI_IFCPI_TF";
                            newNumPortafolio =16;

                        }
                        if (item.NumPortafolio == 11 & item.TipoPosicion == 2)
                        {
                            newPortafolio = "IFCV";
                            newNumPortafolio =13;

                        }
                        if (item.NumPortafolio == 5 & item.TipoPosicion == 1)
                        {
                            newPortafolio = "IFCV";
                            newNumPortafolio =13;

                        }
                        if (item.NumPortafolio == 16 & item.TipoPosicion == 2)
                        {
                            newPortafolio = "IFN";
                            newNumPortafolio =12;

                        }
                        if (item.NumPortafolio == 4 & item.TipoPosicion == 1)
                        {
                            newPortafolio = "IFN";
                            newNumPortafolio =12;

                        }
                        if (item.NumPortafolio == 14)
                        {
                            newPortafolio = "FONDOS DE PENSIONES";
                            newNumPortafolio = 4;

                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("F_POSICION", item.Fecha);
                        cmd.Parameters.Add("INTENCION", item.Intencion);
                        cmd.Parameters.Add("T_OPERACION", item.TipoOperacion);
                        cmd.Parameters.Add("T_VALOR", item.TipoValor);
                        cmd.Parameters.Add("EMISION", item.Emision);
                        cmd.Parameters.Add("SERIE", item.Serie);
                        cmd.Parameters.Add("F_VENCIMIENTO", item.FechaVto);
                        cmd.Parameters.Add("P_CUPON", item.TCupon);
                        cmd.Parameters.Add("D_VTO", item.DxV);
                        cmd.Parameters.Add("T_CUPON", item.TasaCupon);
                        cmd.Parameters.Add("T_PREMIO", item.Premio);
                        cmd.Parameters.Add("N_TITULOS", item.Titulos);
                        cmd.Parameters.Add("M_POSICION", item.TipoPosicion);
                        cmd.Parameters.Add("P_COMPRA", item.PrecioCompra);
                        cmd.Parameters.Add("F_COMPRA", item.FechaCompra);
                        cmd.Parameters.Add("MERCADO", item.Mercado);
                        cmd.Parameters.Add("NUMPORTAFOLIO", newNumPortafolio);
                        cmd.Parameters.Add("NOMPORTAFOLIO", newPortafolio);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESGO.POS_MESA_TESO (F_POSICION" +
                                                                            ",INTENCION" +
                                                                            ",T_OPERACION" +
                                                                            ",T_VALOR" +
                                                                            ",EMISION" +
                                                                            ",SERIE" +
                                                                            ",F_VENCIMIENTO" +
                                                                            ",P_CUPON" +
                                                                            ",D_VTO" +
                                                                            ",T_CUPON" +
                                                                            ",T_PREMIO" +
                                                                            ",N_TITULOS" +
                                                                            ",M_POSICION" +
                                                                            ",P_COMPRA" +
                                                                            ",F_COMPRA" +
                                                                            ",MERCADO" +
                                                                            ",NUMPORTAFOLIO" +
                                                                            ",NOMPORTAFOLIO) " +
                                                                "VALUES (:F_POSICION,:INTENCION,:T_OPERACION,:T_VALOR,:EMISION,:SERIE,:F_VENCIMIENTO,:P_CUPON,:D_VTO,:T_CUPON,:T_PREMIO,:N_TITULOS,:M_POSICION,:P_COMPRA,:F_COMPRA,:MERCADO,:NUMPORTAFOLIO,:NOMPORTAFOLIO)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " resgistros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexionRiesgos.Close();
            }
            return respuesta;
        }

        public Respuesta guardarPosicionRegulatorios(List<PosicionRegulatorios> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarPosicionRegulatorios(Convert.ToInt32(reporte[0].Fecha), conexionRiesgos);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexionRiesgos;

            try
            {
                if (borro)
                {
                    conexionRiesgos.Open();
                    int aff = 0;
                    foreach (PosicionRegulatorios item in reporte)
                    {


                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FECHA_CORTE", item.Fecha);
                        cmd.Parameters.Add("EMISION", item.Emision);
                        cmd.Parameters.Add("TV", item.TV);
                        cmd.Parameters.Add("SERIE", item.Serie);
                        cmd.Parameters.Add("EMISOR", item.Emisor);
                        cmd.Parameters.Add("OP", item.TipoInventario);//se ajusta 
                        cmd.Parameters.Add("FECHA_VAL", item.FechaValor);
                        cmd.Parameters.Add("FECHA_F_OPER", item.FechaVto);
                        cmd.Parameters.Add("NUM_OP", item.NumOper);
                        cmd.Parameters.Add("CPRA_VTA", item.CompraVenta);
                        cmd.Parameters.Add("TITULOS", item.Titulos);
                        cmd.Parameters.Add("PRECIO_LIB", item.PrecioLibros);
                        cmd.Parameters.Add("VALOR_LIB", item.ImporteLibros); //se ajusta 
                        cmd.Parameters.Add("DIAS_TRANS", item.DiasTrans);
                        cmd.Parameters.Add("DXV", item.DxV);
                        cmd.Parameters.Add("TASA_VMTO", item.TasaVto);
                        cmd.Parameters.Add("IMP_VMTO", item.ImporteVto);
                        cmd.Parameters.Add("TASA_CVA_P", item.TasaCurva);
                        cmd.Parameters.Add("PREMIO_DEV", item.PremioDev);
                        cmd.Parameters.Add("TASA_MERCA", item.TasaMercado);
                        cmd.Parameters.Add("VPPV", item.ImporteAcum); //se ajusta 
                        cmd.Parameters.Add("PRECIO_MERC", item.PrecioMercado);//se ajusta 
                        cmd.Parameters.Add("VALOR_MERC", item.ImporteMercado);//se ajusta 
                        cmd.Parameters.Add("PLAZO_PAPE", item.PlazoPapel);
                        cmd.Parameters.Add("TVMTOPAPEL", item.TasaVtoPos); //Se ajusta
                        cmd.Parameters.Add("INTERES_PA", item.Interes);
                        cmd.Parameters.Add("AREA", item.Area);
                        cmd.Parameters.Add("TAS_DIARIA", item.TasaDiaria);
                        cmd.Parameters.Add("CLAVE_EMISOR", item.ClaveEmisor); // anteriormente se consideraba 0
                        cmd.Parameters.Add("T_INST", item.ClaveInstrumento);
                        cmd.Parameters.Add("SECTOR", item.Sector);//anteriormente se consideraba 0
                        cmd.Parameters.Add("DXV_FCORTE", item.DxVCorte);
                        cmd.Parameters.Add("T_OPER", item.TipoOper);
                        cmd.Parameters.Add("CVE_CONT", item.ClaveContraparte);
                        cmd.Parameters.Add("DXV_PAPEL", item.DxVPapel);
                        cmd.Parameters.Add("DURACION", item.Duracion);
                        cmd.Parameters.Add("PLAZO_OPER", item.PlazoOper);
                        cmd.Parameters.Add("VA_DRO", item.VaDro);
                        cmd.Parameters.Add("SOBRETASA", item.SobreTasa);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESGO.LIQ_REPORTOS (FECHA_CORTE" +
                                                                                        ",EMISION" +
                                                                                        ",TV" +
                                                                                        ",SERIE" +
                                                                                        ",EMISOR" +
                                                                                        ",OP" +
                                                                                        ",FECHA_VAL" +
                                                                                        ",FECHA_F_OPER" +
                                                                                        ",NUM_OP" +
                                                                                        ",CPRA_VTA" +
                                                                                        ",TITULOS" +
                                                                                        ",PRECIO_LIB" +
                                                                                        ",VALOR_LIB" +
                                                                                        ",DIAS_TRANS" +
                                                                                        ",DXV" +
                                                                                        ",TASA_VMTO" +
                                                                                        ",IMP_VMTO" +
                                                                                        ",TASA_CVA_P" +
                                                                                        ",PREMIO_DEV" +
                                                                                        ",TASA_MERCA" +
                                                                                        ",VPPV" +
                                                                                        ",PRECIO_MERC" +
                                                                                        ",VALOR_MERC" +
                                                                                        ",PLAZO_PAPE" +
                                                                                        ",TVMTOPAPEL" +
                                                                                        ",INTERES_PA" +
                                                                                        ",AREA" +
                                                                                        ",TAS_DIARIA" +
                                                                                        ",CLAVE_EMISOR" +
                                                                                        ",T_INST" +
                                                                                        ",SECTOR" +
                                                                                        ",DXV_FCORTE" +
                                                                                        ",T_OPER" +
                                                                                        ",CVE_CONT" +
                                                                                        ",DXV_PAPEL" +
                                                                                        ",DURACION" +
                                                                                        ",PLAZO_OPER" +
                                                                                        ",VA_DRO" +
                                                                                        ",SOBRETASA) " +
                                                                                  "VALUES (to_date(:FECHA_CORTE,'yyyymmdd'),:EMISION ,:TV,:SERIE,:EMISOR,:OP, to_date(:FECHA_VAL,'yyyymmdd'), to_date(:FECHA_F_OPER,'yyyymmdd'),:NUM_OP,:CPRA_VTA,:TITULOS,:PRECIO_LIB,:VALOR_LIB,:DIAS_TRANS,:DXV,:TASA_VMTO ,:IMP_VMTO,:TASA_CVA_P,:PREMIO_DEV,:TASA_MERCA,:VPPV,:PRECIO_MERC,:VALOR_MERC,:PLAZO_PAPE,:TVMTOPAPEL,:INTERES_PA,:AREA,:TAS_DIARIA,:CLAVE_EMISOR,:T_INST,:SECTOR,:DXV_FCORTE,:T_OPER,:CVE_CONT,:DXV_PAPEL,:DURACION,:PLAZO_OPER,:VA_DRO,:SOBRETASA)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexionRiesgos.Close();
            }
            return respuesta;
        }

        public Respuesta guardarReportePosicionTesoreria(List<ReportePosicionTesoreria> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarPosicionPosicionTesoreria(Convert.ToInt32(reporte[0].FechaCorte), conexionRiesgos);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexionRiesgos;

            try
            {
                if (borro)
                {
                    conexionRiesgos.Open();
                    int aff = 0;

                    foreach (ReportePosicionTesoreria item in reporte)
                    {
                        string fcort = null;
                        string fvenc = null;
                        string femis = null;
                        string fcte =  null;
                        int newValorMoneda = 0;
                        int ncEmisor = 0;



                        if (item.FechaCorte > 0)
                        {
                            fcort = item.FechaCorte.ToString();
                        }
                        if (item.FechaVto > 0)
                        {
                            //fvenc = Convert.ToString(item.FechaVto);
                            fvenc = item.FechaVto.ToString();
                        }
                        if (item.FechaEmision > 0)
                        {
                            femis = item.FechaEmision.ToString();
                        }
                        if (item.FechaVtoCup > 0)
                        {
                            fcte = item.FechaVtoCup.ToString();
                        }
                        if (item.ClaveEmisor != "")
                        {
                            ncEmisor = Convert.ToInt32(item.ClaveEmisor.ToString());
                        }
                        if( Convert.ToInt32(item.Moneda.ToString()) == 6 ){
                            newValorMoneda = 10;
                        }else{
                            newValorMoneda = Convert.ToInt32(item.Moneda.ToString());
                        }
                        //Se actualiza el cambio de moneda en US
                        if (item.Moneda == "1" && item.Udizado)
                        {
                            newValorMoneda = 2;
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("EMISION", item.Emision);
                        cmd.Parameters.Add("POSICION", item.Posicion);
                        cmd.Parameters.Add("OPERACION", item.Operacion);
                        cmd.Parameters.Add("FECHA_CORTE", fcort);
                        cmd.Parameters.Add("TITULOS", item.Titulos);
                        cmd.Parameters.Add("PRECIO_REF", item.PrecioMercado); // anteriormente se condideraba 0
                        cmd.Parameters.Add("FEC_VENC", fvenc);
                        cmd.Parameters.Add("TASA_MDO", item.TasaMercado);
                        cmd.Parameters.Add("PRECIO_LIBROS", item.PrecioLibros);
                        cmd.Parameters.Add("TASA_CPN", item.TasaCupon);
                        cmd.Parameters.Add("VN", item.ValorNominal);
                        cmd.Parameters.Add("FEC_EMISION", femis);
                        cmd.Parameters.Add("FEC_CTE_CPN", fcte);
                        cmd.Parameters.Add("CVE_INST", item.ClaveInstrumento);
                        cmd.Parameters.Add("CVE_EMISOR", ncEmisor);
                        cmd.Parameters.Add("DURACION", item.Duracion);
                        cmd.Parameters.Add("TIPO_TASA", item.TipoTasa);
                        cmd.Parameters.Add("MONEDA", newValorMoneda);
                        cmd.Parameters.Add("AREA", item.Area);
                        cmd.Parameters.Add("TPZO_OPER", item.Mercado);
                        cmd.Parameters.Add("CVE_CONT", "0");
                        cmd.Parameters.Add("PZO_CPN", item.PlazoCupon);
                        cmd.Parameters.Add("PZO_REPO", item.PlazoRepo);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESGO.LIQ_TESORERIA (EMISION" +
                                                                    ",POSICION" +
                                                                    ",OPERACION" +
                                                                    ",FECHA_CORTE" +
                                                                    ",TITULOS" +
                                                                    ",PRECIO_REF" +
                                                                    ",FEC_VENC" +
                                                                    ",TASA_MDO" +
                                                                    ",PRECIO_LIBROS" +
                                                                    ",TASA_CPN" +
                                                                    ",VN" +
                                                                    ",FEC_EMISION" +
                                                                    ",FEC_CTE_CPN" +
                                                                    ",CVE_INST" +
                                                                    ",CVE_EMISOR" +
                                                                    ",DURACION" +
                                                                    ",TIPO_TASA" +
                                                                    ",MONEDA" +
                                                                    ",AREA" +
                                                                    ",TPZO_OPER" +
                                                                    ",CVE_CONT" +
                                                                    ",PZO_CPN" +
                                                                    ",PZO_REPO) " +
                                                        "VALUES (:EMISION,:POSICION,:OPERACION, to_date(:FECHA_CORTE,'yyyymmdd'),:TITULOS,:PRECIO_REF, to_date(:FEC_VENC,'yyyymmdd'),:TASA_MDO,:PRECIO_LIBROS,:TASA_CPN,:VN, to_date(:FEC_EMISION,'yyyymmdd'), to_date(:FEC_CTE_CPN,'yyyymmdd'),:CVE_INST,:CVE_EMISOR, :DURACION, :TIPO_TASA,:MONEDA,:AREA,:TPZO_OPER,:CVE_CONT, :PZO_CPN,:PZO_REPO)";

                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.exito = false;
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexionRiesgos.Close();
            }
            return respuesta;
        }

        public Respuesta guardarPosicionGlobalTitulos(List<PosicionGlobalTitulos> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarPosicionGlobalTitulos(Convert.ToInt32(reporte[0].Fecha), conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (PosicionGlobalTitulos item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("Fecha", item.Fecha);
                        cmd.Parameters.Add("Emision", item.Emision);
                        cmd.Parameters.Add("TipoInventario", item.TipoInventario);
                        cmd.Parameters.Add("TotalTitulos", item.TotalTitulos);
                        cmd.Parameters.Add("TitulosRepIntermediarios", item.TitulosRepIntermediarios);
                        cmd.Parameters.Add("TitulosGarOtorgados", item.TitulosGarOtorgados);
                        cmd.Parameters.Add("TenenciaIndeval", item.TenenciaIndeval);
                        cmd.Parameters.Add("TitulosReportoCli", item.TitulosReportoCli);
                        cmd.Parameters.Add("TitulosEnAdmon", item.TitulosEnAdmon);
                        cmd.Parameters.Add("TitulosDisponibles", item.TitulosDisponibles);
                        cmd.Parameters.Add("TitulosGarRecibidos", item.TitulosGarRecibidos);
                        cmd.Parameters.Add("TitulosRecibidosCustodia", item.TitulosRecibidosCustodia);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_POSICION_GLOBAL_TIT (Fecha" +
                                                                                        ",Emision" +
                                                                                        ",TipoInventario" +
                                                                                        ",TotalTitulos" +
                                                                                        ",TitulosRepIntermediarios" +
                                                                                        ",TitulosGarOtorgados" +
                                                                                        ",TenenciaIndeval" +
                                                                                        ",TitulosReportoCli" +
                                                                                        ",TitulosEnAdmon" +
                                                                                        ",TitulosDisponibles" +
                                                                                        ",TitulosGarRecibidos" +
                                                                                        ",TitulosRecibidosCustodia)" +
                                                                                        " VALUES (:Fecha,:Emision,:TipoInventario,:TotalTitulos,:TitulosRepIntermediarios,:TitulosGarOtorgados,:TenenciaIndeval,:TitulosReportoCli,:TitulosEnAdmon,:TitulosDisponibles,:TitulosGarRecibidos,:TitulosRecibidosCustodia)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        public Respuesta guardarMovimientosTesoreria(List<MovimientosTesoreria> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarMovimientosTesoreria(Convert.ToInt32(reporte[0].FechaAlta), conexionRiesgos);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexionRiesgos;

            try
            {
                if (borro)
                {
                    conexionRiesgos.Open();
                    int aff = 0;
                    foreach (MovimientosTesoreria item in reporte)
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("IDENTIFICADOR", item.Identificador);
                        cmd.Parameters.Add("NOMINSTRUMENTO", item.NomInstrumento);//0 
                                                                                  //  Console.WriteLine(item.NomInstrumento);
                        cmd.Parameters.Add("EMISOR", item.Emisor);
                        cmd.Parameters.Add("NOMEMI", item.Emision);
                        cmd.Parameters.Add("CLAVEOPER", item.ClaveOper);
                        cmd.Parameters.Add("TIPOMOV", item.TipoMov);
                        cmd.Parameters.Add("NUMTITASIG", item.NumTitAsig);
                        cmd.Parameters.Add("PRECIODEREF", item.PrecioRef);
                        cmd.Parameters.Add("PRECIOLIBROS", item.PrecioLibros);
                        cmd.Parameters.Add("IMPASIG", item.ImporteAsig);
                        cmd.Parameters.Add("TASACOSTO", item.TasaCosto);
                        cmd.Parameters.Add("PLAZO", item.Plazo);
                        cmd.Parameters.Add("FECHAALTA", item.FechaAlta);
                        cmd.Parameters.Add("FECHAVENC", item.FechaVen);
                        cmd.Parameters.Add("TITGARAN", item.TitGarant);
                        cmd.Parameters.Add("PERIODO", item.Periodo);
                        cmd.Parameters.Add("TASAREF", item.TasaRef);
                        cmd.Parameters.Add("EMISIONGAR", item.EmisionGar);
                        cmd.Parameters.Add("NUMFUNCIONARIO", item.NumFuncionario);//0
                        cmd.Parameters.Add("NUMCONTRAPARTE", item.NumContraparte);
                        cmd.Parameters.Add("NUMOPER", item.NumOper);
                        cmd.Parameters.Add("FECHAEXP", item.FechaExp);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESGO.POS_TESO (IDENTIFICADOR" +
                                                                                    ",NOMINSTRUMENTO" +
                                                                                    ",EMISOR" +
                                                                                    ",NOMEMI" +
                                                                                    ",CLAVEOPER" +
                                                                                    ",TIPOMOV" +
                                                                                    ",NUMTITASIG" +
                                                                                    ",PRECIODEREF" +
                                                                                    ",PRECIOLIBROS" +
                                                                                    ",IMPASIG" +
                                                                                    ",TASACOSTO" +
                                                                                    ",PLAZO" +
                                                                                    ",FECHAALTA" +
                                                                                    ",FECHAVENC" +
                                                                                    ",TITGARAN" +
                                                                                    ",PERIODO" +
                                                                                    ",TASAREF" +
                                                                                    ",EMISIONGAR" +
                                                                                    ",NUMFUNCIONARIO" +
                                                                                    ",NUMCONTRAPARTE" +
                                                                                    ",NUMOPER" +
                                                                                    ",FECHAEXP) " +
                                                                           "VALUES (:IDENTIFICADOR,:NOMINSTRUMENTO ,:EMISOR,:NOMEMI,:CLAVEOPER,:TIPOMOV,:NUMTITASIG,:PRECIODEREF,:PRECIOLIBROS,:IMPASIG,:TASACOSTO,:PLAZO, to_date(:FECHAALTA,'yyyymmdd'), to_date(:FECHAVENC,'yyyymmdd'),:TITGARAN,:PERIODO,:TASAREF,:EMISIONGAR,:NUMFUNCIONARIO,:NUMCONTRAPARTE,:NUMOPER, to_date(:FECHAEXP,'yyyymmdd'))";
                        //Se cambia el valor fijo  de 0 en NOMINSTRUMENTO Y NUMFUNCIONARIO

                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.exito = false;
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexionRiesgos.Close();
            }
            return respuesta;
        }

        //REPORTE 17
        public Respuesta guardarLlamadaMargen(List<LlamadaMargen> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarLlamadaMargen(Convert.ToInt32(reporte[0].CveContPP), conexion);

            conexion.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;


            try
            {
                if (borro)
                {
                    //conexion.Open();
                    int aff = 0;
                    foreach (LlamadaMargen item in reporte)
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("CveContPP", item.CveContPP);
                        cmd.Parameters.Add("NombreContra", item.NombreContra);
                        cmd.Parameters.Add("Thresh_C", item.Thresh_C);
                        cmd.Parameters.Add("MMT_C", item.MMT_C);
                        cmd.Parameters.Add("Thresh_B", item.Thresh_B);
                        cmd.Parameters.Add("MMT_B", item.MMT_B);
                        cmd.Parameters.Add("Mon_Calc ", item.Mon_Calc);
                        cmd.Parameters.Add("Val_Simefin ", item.Val_Simefin);
                        cmd.Parameters.Add("Val_Agente", item.Val_Agente);
                        cmd.Parameters.Add("ExDecimal_Neta", item.Exposicion_Neta);
                        cmd.Parameters.Add("Val_Gtias ", item.Val_Gtias);
                        cmd.Parameters.Add("Gtias_Programadas", item.Gtias_Programadas);
                        cmd.Parameters.Add("Llamada_Margen", item.Llamada_Margen);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_LLAMADA_MARGEN (CveContPP" +
                                                                                            ",NombreContra" +
                                                                                            ",Thresh_C" +
                                                                                            ",MMT_C" +
                                                                                            ",Thresh_B" +
                                                                                            ",MMT_B" +
                                                                                            ",Monto" +
                                                                                            ",Mon_Calc" +
                                                                                            ",Val_Simefin" +
                                                                                            ",Val_Agente" +
                                                                                            ",ExDecimal_Neta" +
                                                                                            ",Val_Gtias" +
                                                                                            ",Gtias_Programadas" +
                                                                                            ",Llamada_Margen) " +
                                                                                            "VALUES (:CveContPP,:NombreContra,:Thresh_C,:MMT_C,:Thresh_B,:MMT_B,:Mon_Calc,:Val_Simefin,:Val_Agente,:Exposicion_Neta,:Val_Gtias,:Gtias_Programadas,:Llamada_Margen)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }


        #endregion


        #region Métodos para eliminar ....

        /// <summary>
        /// Reporte # 1
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public bool eliminarValuacionReportos(int fechaConsulta, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                cmd.Parameters.Add("FECHAVALUACION", fechaConsulta);
                cmd.BindByName = true;
                int aff = 0;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_VAL_REPORTOS WHERE FECHAVALUACION = :FECHAVALUACION", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                respuesta.mensaje = ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        /// <summary>
        /// Reporte # 2
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public bool eliminarTenenciaTitulos(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHAVALUACION", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_TENENCIA_TITULOS WHERE FECHAVALUACION = :FECHAVALUACION", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarComprasMesaDinero(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FechaValor", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_COMPRAS_MD WHERE FECHAVALOR = :FechaValor", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarComprasTesoreria(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FechaValor", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_COMPRAS_TESO WHERE FECHAVALOR = :FechaValor", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarPosicionPatrimonial(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FechaValuacion", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_PATRIMONIAL WHERE FECHAVALUACION = :FechaValuacion", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarReporteREVAME(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("Fecha", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_REVAME WHERE FECHA = :Fecha", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarPosicionCalculoVAR(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("F_POSICION", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESGO.POS_MESA_TESO WHERE F_POSICION = :F_POSICION", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarPosicionRegulatorios(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHA_CORTE", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESGO.LIQ_REPORTOS WHERE FECHA_CORTE = to_date(:FECHA_CORTE,'yyyymmdd')", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarPosicionPosicionTesoreria(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHA_CORTE", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESGO.LIQ_TESORERIA WHERE FECHA_CORTE = to_date(:FECHA_CORTE,'yyyymmdd')", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarPosicionGlobalTitulos(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHA", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_POSICION_GLOBAL_TIT WHERE FECHA = :FECHA", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        public bool eliminarMovimientosTesoreria(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHAALTA", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESGO.POS_TESO WHERE FECHAALTA = to_date(:FECHAALTA,'yyyymmdd')", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        /// <summary>
        /// Reporte # 17
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public bool eliminarLlamadaMargen(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("CveContPP", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_LLAMADA_MARGEN WHERE CveContPP = :CveContPP", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }


        #endregion


        #region Reporte # 13

        /// <summary>
        /// Reporte # 13
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public Respuesta guardarPosicionForwards(List<PosicionForwards> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            //borro = eliminarPosicionForwards(Convert.ToInt32(reporte[0].F_Posicion), conexion);
            respuesta = this.eliminarPosicionForwards(Convert.ToInt32(reporte[0].F_Posicion), conexion);
            if (!string.IsNullOrEmpty(respuesta.mensaje))
            {
                return respuesta;
            }

            try
            {
                conexion.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conexion;

                int aff = 0;
                string newClaveProducto;
                foreach (PosicionForwards item in reporte)
                {

                    if (item.Clave_Producto == "")
                    {
                        newClaveProducto = "null";
                    }
                    else
                    {
                        newClaveProducto = item.Clave_Producto;
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("F_POSICION", item.F_Posicion);
                    cmd.Parameters.Add("CLAVE_OP", item.Clave_Op);
                    cmd.Parameters.Add("T_OPERACION", item.T_Operacion);
                    cmd.Parameters.Add("M_NOCIONAL", item.M_Nocional);
                    cmd.Parameters.Add("F_INICIO", item.F_inicio);
                    cmd.Parameters.Add("F_VENCIMIENTO", item.F_Vencimiento);
                    cmd.Parameters.Add("F_LIQUIDACION", item.F_Liquidacion);
                    cmd.Parameters.Add("CLAVE_PRODUCTO", newClaveProducto);
                    cmd.Parameters.Add("PLAZO_FWD", item.Plazo_Fwd);
                    cmd.Parameters.Add("TC_PACTADO", item.Tc_Pactado);
                    cmd.Parameters.Add("INTENCION", item.Intencion);
                    cmd.Parameters.Add("LIQUIDACION", item.Liquidacion);
                    cmd.Parameters.Add("VALUACION", item.Valuacion);
                    cmd.Parameters.Add("CONTRAPARTE", item.Contraparte);
                    cmd.Parameters.Add("NEGO_ESTRUC", item.Nego_Estruc);


                    cmd.CommandText = "INSERT INTO XYZWIN.OPER_FWDS (F_POSICION" +
                                                            ",CLAVE_OP" +
                                                            ",T_OPERACION" +
                                                            ",M_NOCIONAL" +
                                                            ",F_INICIO" +
                                                            ",F_VENCIMIENTO" +
                                                            ",F_LIQUIDACION" +
                                                            ",CLAVE_PRODUCTO" +
                                                            ",PLAZO_FWD" +
                                                            ",TC_PACTADO" +
                                                            ",INTENCION" +
                                                            ",LIQUIDACION" +
                                                            ",VALUACION" +
                                                            ",CONTRAPARTE" +
                                                            ",NEGO_ESTRUC) " +
                                                            "VALUES (to_date(:F_POSICION,'yyyymmdd'),:CLAVE_OP,:T_OPERACION,:M_NOCIONAL,to_date(:F_INICIO,'yyyymmdd'),to_date(:F_VENCIMIENTO,'yyyymmdd'),to_date(:F_LIQUIDACION,'yyyymmdd'),:CLAVE_PRODUCTO,:PLAZO_FWD,:TC_PACTADO,:INTENCION,:LIQUIDACION,:VALUACION,:CONTRAPARTE,:NEGO_ESTRUC)";

                    cmd.BindByName = true;

                    cmd.CommandType = CommandType.Text;
                    aff += cmd.ExecuteNonQuery();
                }

                respuesta.exito = true;
                respuesta.mensaje = aff + " registros fueron insertados.";

            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        /// <summary>
        /// Reporte # 13
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        private Respuesta eliminarPosicionForwards(int fechaConsulta, OracleConnection conexion)
        {
            int affectedRecords = 0;
            Respuesta response = new Respuesta();
            response.exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                cmd.Parameters.Add("F_Posicion", fechaConsulta);
                cmd.BindByName = true;
                string strSQL = String.Format("DELETE FROM XYZWIN.OPER_FWDS WHERE F_Posicion = to_date(:F_Posicion,'yyyymmdd')", fechaConsulta);
                cmd.CommandText = strSQL;

                affectedRecords = cmd.ExecuteNonQuery();
                response.exito = affectedRecords >= 0;
            }
            catch (Exception ex)
            {
                response.exito = false;
                response.mensaje = ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return response;
        }

        #endregion


        #region Reporte # 14



        //REPORTE 14
        public Respuesta guardarFlujosSwaps(List<FlujosSwaps> reporte, List<CatalogoDivisas> catalogoDivisas, OracleConnection conexion)
        {

            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarFlujoswaps(Convert.ToInt32(reporte[0].Fecha_Pos), conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;


            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (FlujosSwaps item in reporte)
                    {
                        cmd.CommandTimeout = 0;

                        string? newFechaLiq = null;
                        if (item.Fec_Liq != 0)
                        {
                            newFechaLiq = Convert.ToString(item.Fec_Liq);
                        }


                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FECHA_POS", item.Fecha_Pos);
                        cmd.Parameters.Add("CVESWAP", item.Cveswap);
                        cmd.Parameters.Add("POSICION", item.Posicion);
                        cmd.Parameters.Add("SECUENCIA", item.Secuencia);
                        cmd.Parameters.Add("TIP_NEGOC", item.Tip_Negoc);
                        cmd.Parameters.Add("FEC_INI", item.Fec_Ini);
                        cmd.Parameters.Add("FEC_TER", item.Fec_Ter);
                        cmd.Parameters.Add("VALOR_NOC", item.Valor_Noc);
                        cmd.Parameters.Add("AMORTIZACION", item.Amortizacion);
                        cmd.Parameters.Add("TASA", item.Tasa);
                        cmd.Parameters.Add("SPREAD", item.Spread);
                        cmd.Parameters.Add("CUPON", item.Cupon);
                        cmd.Parameters.Add("CONVENCION", item.Convencion);
                        cmd.Parameters.Add("NUMMON", catalogoDivisas.FirstOrDefault(x => x.Id_Apesa == item.Nummon)!.Id_Sivarmer);
                        cmd.Parameters.Add("TIP_SWAP", item.Tip_Swap);
                        cmd.Parameters.Add("ID_SIDE", item.Id_Side);
                        cmd.Parameters.Add("INTERI", item.Interi);
                        cmd.Parameters.Add("INTERF", item.Interf);
                        cmd.Parameters.Add("FEC_LIQ", newFechaLiq);
                        cmd.Parameters.Add("NEGO_ESTRUC", item.Nego_Estruc);
                        cmd.Parameters.Add("PAGA_INT", item.Paga_int);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO XYZWIN.SW_RIESGOS1_TEMP (FECHA_POS" +
                                                                                     ",CVESWAP" +
                                                                                    ",POSICION" +
                                                                                    ",SECUENCIA" +
                                                                                    ",TIP_NEGOC" +
                                                                                    ",FEC_INI" +
                                                                                    ",FEC_TER" +
                                                                                    ",VALOR_NOC" +
                                                                                    ",AMORTIZACION" +
                                                                                    ",TASA" +
                                                                                    ",SPREAD" +
                                                                                     ",CUPON" +
                                                                                     ",NUMMON" +
                                                                                     ",TIP_SWAP" +
                                                                                     ",ID_SIDE" +
                                                                                     ",INTERI" +
                                                                                     ",INTERF" +
                                                                                     ",FEC_LIQ" +
                                                                                     ",NEGO_ESTRUC" +
                                                                                     ",PAGA_INT) " +
                                                                                      "VALUES (to_date(:FECHA_POS,'yyyymmdd'),:CVESWAP,:POSICION,:SECUENCIA,:TIP_NEGOC,to_date(:FEC_INI,'yyyymmdd'),to_date(:FEC_TER,'yyyymmdd'),:VALOR_NOC,:AMORTIZACION,:TASA,:SPREAD,:CUPON,:NUMMON,:TIP_SWAP,:ID_SIDE,:INTERI,:INTERF,to_date(:FEC_LIQ,'yyyymmdd'),:NEGO_ESTRUC,:PAGA_INT)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";

                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        /// <summary>
        /// Reporte # 14
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public bool eliminarFlujoswaps(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHA_POS", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM XYZWIN.SW_RIESGOS1_TEMP WHERE FECHA_POS = to_date(:FECHA_POS,'yyyymmdd')", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        #endregion


        #region Reporte # 15

        /// <summary>
        /// Reporte # 15
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public async Task<Respuesta> FlujosPosicionesPrimariasProcess(int fechaConsulta, List<FlujosPosicionesPrimarias> mirrorDataList, OracleConnection conexion)
        {
            Respuesta response = new Respuesta { exito = false, ListaMensajes = new List<string>() };
            Respuesta respuesta = new Respuesta { exito = false, ListaMensajes = new List<string>() };

            //Filtrando por CPosicion = 50
            mirrorDataList = mirrorDataList.Where(x => x.TipoPos == 1 && x.CPosicion == 50).ToList();
            if (mirrorDataList.Count == 0)
            {
                response.exito = true;
                response.mensaje = "No se encontraron registros para procesar.";
                return response;
            }

            try
            {
                //Obteniendo el conjunto de datos de la tabla productiva
                //var productionDataList = await this.GetProductionData15(fechaConsulta.ToString(), conexion);
                var productionDataList = await this.GetProductionData_15(DateTime.UtcNow.AddDays(-1).ToString("yyyyMMdd"), conexion);

                //Agrupando datos espejo por COperacion
                var mirrorDataGroups = mirrorDataList.GroupBy(x => x.COperacion).ToList();
                foreach (var groupCOperacion in mirrorDataGroups)
                {
                    string currentCOperacion = groupCOperacion.Key;
                    var currentMirrorList_B = groupCOperacion.Where(x => x.T_Pata == "B").ToList();
                    var currentProductionList_B = productionDataList.Where(x => x.COperacion == currentCOperacion && x.T_Pata == "B").ToList();
                    var currentMirrorList_C = groupCOperacion.Where(x => x.T_Pata == "C").ToList();
                    var currentProductionList_C = productionDataList.Where(x => x.COperacion == currentCOperacion && x.T_Pata == "C").ToList();

                    //Si el conteo entre espejo y productivo es diferente o en producción no hay registros, insertar todo
                    if ((currentProductionList_B.Count + currentProductionList_C.Count == 0) ||
                        ((currentMirrorList_B.Count + currentMirrorList_C.Count) != (currentProductionList_B.Count + currentProductionList_C.Count)))
                    {
                        respuesta = this.SaveFlujosPosicionesPrimarias_15(groupCOperacion.ToList(), conexion);
                        response.ListaMensajes.Add($"COperacion = { currentCOperacion }, " + respuesta.mensaje);
                        continue;
                    }

                    //1. Procesando información con T_PATA => B
                    if (currentMirrorList_B.Count != currentProductionList_B.Count)
                    {
                        //Si hay diferencia, insertar todos los registros espejo en tabla productiva
                        respuesta =  this.SaveFlujosPosicionesPrimarias_15(currentMirrorList_B, conexion);
                        response.ListaMensajes.Add(respuesta.mensaje);
                    }
                    else
                    {
                        //3. Si el conteo es igual, ejecuta la comparación entre campos y actualiza aquellos que hayan sufrido un cambio
                        //Hacer distinción por los campos F_INICIO, F_FINAL, PAGO_INT, INT_S_SALDO, SALDO, AMORTIZACION, T_APLICAR.
                        var newDataList = currentMirrorList_B.Where(sc => !currentProductionList_B.Any(dc =>
                                                                                this.ConvertToDecimal(dc.Amortizacion.ToString()) == this.ConvertToDecimal(sc.Amortizacion.ToString()) &&
                                                                                this.ConvertToDate(dc.F_Final.ToString(), 2) == this.ConvertToDate(sc.F_Final.ToString(), 2) &&
                                                                                this.ConvertToDate(dc.F_Inicio.ToString(), 2) == this.ConvertToDate(sc.F_Inicio.ToString(), 2) &&
                                                                                this.ConvertToStr(dc.Int_S_Saldo.ToString()) == this.ConvertToStr(sc.Int_S_Saldo.ToString()) &&
                                                                                this.ConvertToStr(dc.Pago_int.ToString()) == this.ConvertToStr(sc.Pago_int.ToString()) &&
                                                                                this.ConvertToDecimal(dc.Saldo.ToString()) == this.ConvertToDecimal(sc.Saldo.ToString()) &&
                                                                                this.ConvertToDecimal(dc.T_Aplicar.ToString()) == this.ConvertToDecimal(sc.T_Aplicar.ToString())
                                                                    )).ToList();
                        if (newDataList.Any() && newDataList.Count > 0)
                        {
                            respuesta = this.SaveFlujosPosicionesPrimarias_15(newDataList, conexion);
                            response.ListaMensajes.Add(respuesta.mensaje);
                        }
                    }

                    //2. Procesando información con T_PATA => C
                    if (currentMirrorList_C.Count != currentProductionList_C.Count)
                    {
                        //Si hay diferencia, insertar todos los registros espejo en tabla productiva
                        respuesta = this.SaveFlujosPosicionesPrimarias_15(currentMirrorList_C, conexion);
                        response.ListaMensajes.Add(respuesta.mensaje);
                    }
                    else
                    {
                        //3. Si el conteo es igual, ejecuta la comparación entre campos y actualiza aquellos que hayan sufrido un cambio
                        //Hacer distinción por los campos F_INICIO, F_FINAL, PAGO_INT, INT_S_SALDO, SALDO, AMORTIZACION, T_APLICAR.
                        var newDataList = currentMirrorList_C.Where(sc => !currentProductionList_C.Any(dc =>
                                                                                this.ConvertToDecimal(dc.Amortizacion.ToString()) == this.ConvertToDecimal(sc.Amortizacion.ToString()) &&
                                                                                this.ConvertToDate(dc.F_Final.ToString(), 2) == this.ConvertToDate(sc.F_Final.ToString(), 2) &&
                                                                                this.ConvertToDate(dc.F_Inicio.ToString(), 2) == this.ConvertToDate(sc.F_Inicio.ToString(), 2) &&
                                                                                this.ConvertToStr(dc.Int_S_Saldo.ToString()) == this.ConvertToStr(sc.Int_S_Saldo.ToString()) &&
                                                                                this.ConvertToStr(dc.Pago_int.ToString()) == this.ConvertToStr(sc.Pago_int.ToString()) &&
                                                                                this.ConvertToDecimal(dc.Saldo.ToString()) == this.ConvertToDecimal(sc.Saldo.ToString()) &&
                                                                                this.ConvertToDecimal(dc.T_Aplicar.ToString()) == this.ConvertToDecimal(sc.T_Aplicar.ToString())
                                                                    )).ToList();
                        if (newDataList.Any() && newDataList.Count > 0)
                        {
                            respuesta = this.SaveFlujosPosicionesPrimarias_15(newDataList, conexion);
                            response.ListaMensajes.Add(respuesta.mensaje);
                        }
                    }
                }
                response.exito = true;
            }
            catch (Exception ex)
            {
                response.mensaje = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Reporte # 15 - Obtener lista de registros en tabla productiva
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        private async Task<List<FlujosPosicionesPrimarias>> GetProductionData_15(string fechaConsulta, OracleConnection conexion)
        {
            List<FlujosPosicionesPrimarias> productionDataList = new List<FlujosPosicionesPrimarias>();

            try
            {
                //string strSQL = String.Format("SELECT * FROM RIESVARM.VAR_TD_FLUJOS_SWAPS WHERE TIPOPOS = 1 AND CPOSICION = 50 AND (T_PATA = 'B' OR T_PATA = 'C') AND FECHAREG = to_date(:FECHAREG,'yyyymmdd')", fechaConsulta);
                  string strSQL = String.Format("SELECT * FROM RIESVARM.VAR_TD_FLUJOS_SWAPS_BK WHERE TIPOPOS = 1 AND CPOSICION = 50 AND (T_PATA = 'B' OR T_PATA = 'C') AND FECHAREG = to_date(:FECHAREG,'yyyymmdd')", fechaConsulta);
                using (OracleCommand cmd = new OracleCommand(strSQL, conexion))
                {
                    conexion.Open(); 
                    cmd.Parameters.Add("FECHAREG", fechaConsulta!);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.BindByName = true;

                    var reader = await cmd.ExecuteReaderAsync();

                    IEnumerable<FlujosPosicionesPrimarias> result = reader
                                                                    .Cast<IDataRecord>()
                                                                    .Select(dr => new FlujosPosicionesPrimarias
                                                                    {
                                                                        Amortizacion = (decimal)this.ConvertToDecimal(dr["Amortizacion"].ToString()!)!,
                                                                        COperacion = (string)dr["COperacion"],
                                                                        CPosicion = (int)this.ConvertToInt(dr["CPosicion"].ToString()!)!,
                                                                        F_Desc = (int)this.ConvertToDate(dr["F_Desc"].ToString()!, 1)!,
                                                                        F_Final = (int)this.ConvertToDate(dr["F_Final"].ToString()!, 1)!,
                                                                        F_Fixing = (int)this.ConvertToDate(dr["F_Fixing"].ToString()!, 1)!,
                                                                        F_Inicio = (int)this.ConvertToDate(dr["F_Inicio"].ToString()!, 1)!,
                                                                        Fechareg = (int)this.ConvertToDate(dr["FechaReg"].ToString()!, 1)!,
                                                                        HoraReg = (string)dr["HoraReg"],
                                                                        Int_S_Saldo = (string)dr["Int_S_Saldo"],
                                                                        NomPos = (string)dr["NomPos"],
                                                                        Pago_int = (string)dr["Pago_Int"],
                                                                        Saldo = (decimal)this.ConvertToDecimal(dr["Saldo"].ToString()!)!,
                                                                        T_Aplicar = (decimal)this.ConvertToDecimal(dr["T_Aplicar"].ToString()!)!,
                                                                        T_Pata = (string)dr["T_Pata"],
                                                                        TipoPos = (int)this.ConvertToInt(dr["TipoPos"].ToString()!)!
                                                                    });

                    productionDataList = result.ToList();
                    await reader.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return productionDataList;
        }

        /// <summary>
        /// Reporte # 15 - Guardar información de Flujos de Posiciones Primarias
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="dateSave"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        private Respuesta SaveFlujosPosicionesPrimarias_15(List<FlujosPosicionesPrimarias> dataList, OracleConnection conexion)
        {
            Respuesta response = new Respuesta { exito = false };
            string strDateSave = DateTime.UtcNow.AddDays(-1).ToString("yyyyMMdd");

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            //OracleTransaction transaction = conexion.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            //cmd.Transaction = transaction;
            int affectedRecords = 0;
            OracleTransaction? transaction = null;

            try
            {
                conexion.Open();
                transaction = conexion.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                cmd.Transaction = transaction;
                cmd.CommandTimeout = 0;

                foreach (FlujosPosicionesPrimarias item in dataList)
                {
                    //cmd.CommandTimeout = 0;
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("TIPOPOS", item.TipoPos);
                    cmd.Parameters.Add("FECHAREG", strDateSave);
                    cmd.Parameters.Add("NOMPOS", item.NomPos);
                    //cmd.Parameters.Add("HORAREG", DateTime.UtcNow.ToString("HH:mm:ss"));
                    cmd.Parameters.Add("HORAREG", "00000");
                    cmd.Parameters.Add("CPOSICION", item.CPosicion);
                    cmd.Parameters.Add("COPERACION", item.COperacion);
                    cmd.Parameters.Add("T_PATA", item.T_Pata);
                    //cmd.Parameters.Add("F_FIXING", item.F_Fixing);
                    cmd.Parameters.Add("F_FIXING", item.F_Inicio);
                    cmd.Parameters.Add("F_INICIO", item.F_Inicio);
                    cmd.Parameters.Add("F_FINAL", item.F_Final);
                    //cmd.Parameters.Add("F_DESC", item.F_Desc);
                    cmd.Parameters.Add("F_DESC", item.F_Final);
                    cmd.Parameters.Add("PAGO_INT", item.Pago_int);
                    cmd.Parameters.Add("INT_S_SALDO", item.Int_S_Saldo);
                    cmd.Parameters.Add("SALDO", item.Saldo);
                    cmd.Parameters.Add("AMORTIZACION", item.Amortizacion);
                    cmd.Parameters.Add("T_APLICAR", item.T_Aplicar);
                    cmd.BindByName = true;
                    //cmd.CommandText = "INSERT INTO RIESVARM.VAR_TD_FLUJOS_SWAPS (TIPOPOS" +
                    cmd.CommandText = "INSERT INTO RIESVARM.VAR_TD_FLUJOS_SWAPS_BK (TIPOPOS" +
                                                                                    ",FECHAREG" +
                                                                                    ",NOMPOS" +
                                                                                    ",HORAREG" +
                                                                                    ",CPOSICION" +
                                                                                    ",COPERACION" +
                                                                                    ",T_PATA" +
                                                                                    ",F_FIXING" +
                                                                                    ",F_INICIO" +
                                                                                    ",F_FINAL" +
                                                                                    ",F_DESC" +
                                                                                    ",PAGO_INT" +
                                                                                    ",INT_S_SALDO" +
                                                                                    ",SALDO" +
                                                                                    ",AMORTIZACION" +
                                                                                    ",T_APLICAR) " +
                                                                                    "VALUES (:TIPOPOS,to_date(:FECHAREG ,'yyyymmdd'),:NOMPOS,:HORAREG,:CPOSICION,:COPERACION,:T_PATA,to_date(:F_FIXING,'yyyymmdd'),to_date(:F_INICIO,'yyyymmdd'),to_date(:F_FINAL,'yyyymmdd'),to_date(:F_DESC,'yyyymmdd'),:PAGO_INT,:INT_S_SALDO,:SALDO,:AMORTIZACION,:T_APLICAR)";
                    cmd.CommandType = CommandType.Text;
                    affectedRecords += cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                response.exito = true;
                response.mensaje = affectedRecords + " registros fueron insertados.";
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                response.mensaje = $"{ affectedRecords } registros fueron revertidos. " + "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }

            return response;
        }

        #endregion


        #region Reporte # 16

        //REPORTE 16
        public Respuesta guardarCaracteristicasSwaps(List<CaracteristicasSwaps> reporte, List<CatalogoDivisas> catalogoDivisas, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarCaracteristicasSwaps(Convert.ToInt32(reporte[0].Fecha_Pos), conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;



            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (CaracteristicasSwaps item in reporte)
                    {

                        int newNumSec = 0;
                        int newCvecontpp = 0;
                        int newBaseCalcTasa = 0;
                        string moneda = item.Cvemoneda;
                        int newCvemoneda;
                        int newBaseCalculo = 0;
                        int newTipoMoneda = 0;
                        int newF_Prox_Flujo_Ent = 0;
                        int newF_Prox_Flujo = 0;

                        string newTasa_Activa = item.Tasa_Ref_Activa; //"";
                        string newTasa_Pasiva = item.Tasa_Ref_Pasiva;  //"";

                        if (!string.IsNullOrEmpty(item.Tasa_Ref_Activa) && !char.IsDigit(item.Tasa_Ref_Activa[0]))
                        {
                            if (item.T_Ref_Activa_Dias_Ante == null)
                            {
                                int cte = 0;
                                newTasa_Activa = item.Tasa_Ref_Activa + "[" + cte + "]";
                            }
                            else
                            {
                                newTasa_Activa = item.Tasa_Ref_Activa + "[" + item.T_Ref_Activa_Dias_Ante + "]";
                            }
                        }

                        if (!string.IsNullOrEmpty(item.Tasa_Ref_Pasiva) && !char.IsDigit(item.Tasa_Ref_Pasiva[0]))
                        {
                            if (item.T_Ref_Pasiva_Dias_Ante == null)
                            {
                                int cte = 0;
                                newTasa_Pasiva = item.Tasa_Ref_Pasiva + "[" + cte + "]";
                            }
                            else
                            {
                                newTasa_Pasiva = item.Tasa_Ref_Pasiva + "[" + item.T_Ref_Pasiva_Dias_Ante + "]";
                            }
                        }

                        if (!string.IsNullOrEmpty(item.Numsec))
                        {
                            newNumSec = 0;
                        }

                        if (!string.IsNullOrEmpty(item.Cvecontpp))
                        {
                            newCvecontpp = Convert.ToInt32(item.Cvecontpp.ToString());
                        }

                        if (!string.IsNullOrEmpty(item.Base_Cal_Tasa))
                        {
                            newBaseCalcTasa = Convert.ToInt32(item.Base_Cal_Tasa.ToString());
                        }

                        if (item.Cvemoneda != "MXN")
                        {
                            newCvemoneda = 1;
                        }
                        else
                        {
                            newCvemoneda = 2;
                        }

                        if (!string.IsNullOrEmpty(item.Base_Calculo))
                        {
                            newBaseCalculo = Convert.ToInt32(item.Base_Calculo.ToString());
                        }

                        if (!string.IsNullOrEmpty(item.Tipo_Moneda))
                        {
                            newTipoMoneda = 1;
                        }

                        if (item.F_Prox_Flujo_Ent != null)
                        {
                            newF_Prox_Flujo_Ent = 20230313;
                        }

                        if (item.Fec_Prox_Flujo != null)
                        {
                            newF_Prox_Flujo = 20230313;
                        }


                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FECHA_POS", item.Fecha_Pos);
                        cmd.Parameters.Add("PERIODO", item.Periodo);
                        cmd.Parameters.Add("CVE_INST", item.Cve_Inst);
                        cmd.Parameters.Add("CVE_PROV", item.Cve_Prov);
                        cmd.Parameters.Add("NUMSEC", newNumSec);
                        cmd.Parameters.Add("CVECONTPP", newCvecontpp);
                        cmd.Parameters.Add("FEC_OPERAC", item.Fec_Operac);
                        cmd.Parameters.Add("FEC_INI", item.Fec_Ini);
                        cmd.Parameters.Add("FEC_VENC", item.Fec_Venc);
                        cmd.Parameters.Add("TIPO_VALOR", item.Tipo_Valor);
                        cmd.Parameters.Add("TIPO_OPER", item.Tipo_Oper);
                        cmd.Parameters.Add("FEC_PROX_FLUJO", newF_Prox_Flujo_Ent);
                        cmd.Parameters.Add("PERIODO_FLUJO", item.Periodo_Flujo);
                        cmd.Parameters.Add("BASE_CAL_TASA", newBaseCalcTasa);
                        cmd.Parameters.Add("CVEMONEDA", catalogoDivisas.FirstOrDefault(x => x.Id_Apesa == item.Cvemoneda)!.Id_Sivarmer);
                        cmd.Parameters.Add("TIPO_CAMBIO_REC", item.Tipo_Cambio_Rec);
                        cmd.Parameters.Add("M_NOC_FLUJO", item.M_Noc_Flujo);
                        cmd.Parameters.Add("FORMULA_FLUJO", item.Formula_Flujo);
                        cmd.Parameters.Add("TASA_RECIBE", item.Tasa_Recibe);
                        cmd.Parameters.Add("F_PROX_FLUJO_ENT", newF_Prox_Flujo_Ent);
                        cmd.Parameters.Add("PERIODICIDAD", item.Periodicidad);
                        cmd.Parameters.Add("BASE_CALCULO", newBaseCalculo);
                        cmd.Parameters.Add("TIPO_MONEDA", catalogoDivisas.FirstOrDefault(x => x.Id_Apesa == item.Tipo_Moneda)!.Id_Sivarmer);
                        cmd.Parameters.Add("TIPO_CAMBIO_ENT", item.Tipo_Cambio_Ent);
                        cmd.Parameters.Add("M_NOC_FLUJO_ENT", item.M_Noc_Flujo_Ent);
                        cmd.Parameters.Add("TASA_REF_ACTIVA", newTasa_Activa);
                        cmd.Parameters.Add("TASA_REF_PASIVA", newTasa_Pasiva);
                        cmd.Parameters.Add("SOBRETASA_ACTIVA", item.Sobretasa_Activa);
                        cmd.Parameters.Add("SOBRETASA_PASIVA", item.Sobretasa_Pasiva);
                        cmd.Parameters.Add("TASA_ENTREGA", item.Tasa_Entrega);
                        cmd.Parameters.Add("PRIMA", item.Prima);
                        cmd.Parameters.Add("OBJETIVO_OPER", item.Objetivo_Oper);
                        cmd.Parameters.Add("VAL_ACTIVA", item.Val_Activa);
                        cmd.Parameters.Add("VAL_PASIVA", item.Val_Pasiva);
                        cmd.Parameters.Add("VALOR_NETO", item.Valor_Neto);
                        cmd.Parameters.Add("MARCA_MERCADO", item.Marca_Mercado);
                        cmd.Parameters.Add("ID_SIDE", item.Id_Side);
                        cmd.Parameters.Add("LLAMA_MARGEN", item.Llama_Margen);
                        cmd.Parameters.Add("NEGO_ESTRUC", item.Nego_Estruc);
                        cmd.Parameters.Add("REINVIERTE_INT_ACT", item.Reinvierte_int_Act);
                        cmd.Parameters.Add("REINVIERTE_INT_PAS", item.Reinvierte_int_Pas);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO XYZWIN.SW_RIESGOS2_TEMP (FECHA_POS" +
                                                                               ",PERIODO" +
                                                                               ",CVE_INST" +
                                                                               ",CVE_PROV" +
                                                                               ",NUMSEC" +
                                                                               ",CVECONTPP" +
                                                                               ",FEC_OPERAC" +
                                                                               ",FEC_INI" +
                                                                               ",FEC_VENC" +
                                                                               ",TIPO_VALOR" +
                                                                               ",TIPO_OPER" +
                                                                               ",FEC_PROX_FLUJO" +
                                                                               ",PERIODO_FLUJO" +
                                                                               ",BASE_CAL_TASA" +
                                                                               ",CVEMONEDA" +
                                                                               ",TIPO_CAMBIO_REC" +
                                                                               ",M_NOC_FLUJO" +
                                                                               ",FORMULA_FLUJO" +
                                                                               ",TASA_RECIBE" +
                                                                               ",F_PROX_FLUJO_ENT" +
                                                                               ",PERIODICIDAD" +
                                                                               ",BASE_CALCULO" +
                                                                               ",TIPO_MONEDA" +
                                                                               ",TIPO_CAMBIO_ENT" +
                                                                               ",M_NOC_FLUJO_ENT" +
                                                                               ",TASA_REF_ACTIVA" +
                                                                               ",TASA_REF_PASIVA" +
                                                                               ",SOBRETASA_ACTIVA" +
                                                                               ",SOBRETASA_PASIVA" +
                                                                               ",TASA_ENTREGA" +
                                                                               ",PRIMA" +
                                                                               ",OBJETIVO_OPER" +
                                                                               ",VAL_ACTIVA" +
                                                                               ",VAL_PASIVA" +
                                                                               ",VALOR_NETO" +
                                                                               ",MARCA_MERCADO" +
                                                                               ",ID_SIDE" +
                                                                               ",LLAMA_MARGEN" +
                                                                               ",NEGO_ESTRUC" +
                                                                               ",REINVIERTE_INT_ACT" +
                                                                               ",REINVIERTE_INT_PAS ) " +
                                                                               "VALUES (to_date(:FECHA_POS,'yyyymmdd'),:PERIODO,:CVE_INST,:CVE_PROV,:NUMSEC,:CVECONTPP,to_date(:FEC_OPERAC,'yyyymmdd'),to_date(:FEC_INI,'yyyymmdd'),to_date(:FEC_VENC,'yyyymmdd'),:TIPO_VALOR,:TIPO_OPER,to_date(:FEC_PROX_FLUJO,'yyyymmdd'),:PERIODO_FLUJO,:BASE_CAL_TASA,:CVEMONEDA,:TIPO_CAMBIO_REC,:M_NOC_FLUJO,:FORMULA_FLUJO,:TASA_RECIBE,to_date(:F_PROX_FLUJO_ENT,'yyyymmdd'),:PERIODICIDAD,:BASE_CALCULO,:TIPO_MONEDA,:TIPO_CAMBIO_ENT,:M_NOC_FLUJO_ENT,:TASA_REF_ACTIVA,:TASA_REF_PASIVA,:SOBRETASA_ACTIVA,:SOBRETASA_PASIVA,:TASA_ENTREGA,:PRIMA,:OBJETIVO_OPER,:VAL_ACTIVA,:VAL_PASIVA,:VALOR_NETO,:MARCA_MERCADO,:ID_SIDE,:LLAMA_MARGEN,:NEGO_ESTRUC,:REINVIERTE_INT_ACT,:REINVIERTE_INT_PAS)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        /// <summary>
        /// Reporte # 16
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public bool eliminarCaracteristicasSwaps(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHA_POS", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM XYZWIN.SW_RIESGOS2_TEMP WHERE FECHA_POS = to_date(:FECHA_POS,'yyyymmdd') ", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        #endregion


        #region Reporte # 18

        /// <summary>
        /// Reporte # 18
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public async Task<Respuesta> guardarPosicionPrimariaSwaps(int fechaConsulta, List<PosicionPrimariaSwaps> mirrorDataList, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta { exito = false };
            string? newFechareg = mirrorDataList[0].Fechareg == null ? DateTime.Now.AddDays(-1).ToString("yyyyMMdd") : mirrorDataList[0].Fechareg.ToString();

            respuesta = this.eliminarPosicionPrimariasSwaps18(fechaConsulta, conexion);
            //respuesta = this.eliminarPosicionPrimariasSwaps18(Convert.ToInt32(newFechareg), conexion);
            if (!string.IsNullOrEmpty(respuesta.mensaje))
            {
                return respuesta;
            }

            List<PosicionPrimariaSwaps> newDataList = new List<PosicionPrimariaSwaps>();
            //Obteniendo el conjunto de datos de la tabla productiva
            //var productionDataList = await this.GetProductionData18(fechaConsulta.ToString(), conexion);
            var productionDataList = await this.GetProductionData18(newFechareg, conexion);
            //var results = await Task.Run(() => this.GetProductionData(newFechareg!, conexion));
            if (productionDataList.Any() && productionDataList.Count > 0)
            {
                //Haciendo la comparativa por los campos:
                // FINICIO, FVENCIMIENTO, INTER_I, INTER_F, AC_INT_ACT, AC_INT_PAS, TC_ACTIVA, TC_PASIVA, ST_ACTIVA, ST_PASIVA, CONV_INT_ACT, CONV_INT_PAS
                // F_VALUACION, ID_CONTRAP, ID_BANXICO, LLAMA_MARGEN, INTENCION, ESTRUCTURAL, CAL_F_T_ACTIVA, CAL_F_T_PASIVA, CAL_LIQ_ACTIVA, CAL_LIQ_PASIVA, COLATERAL
                newDataList = mirrorDataList.Where(sc => !productionDataList.Any(dc =>
                                                                                    dc.Ac_Int_Act == sc.Ac_Int_Act &&
                                                                                    dc.Ac_Int_Pas == sc.Ac_Int_Pas &&
                                                                                    dc.Cal_F_T_Activa == sc.Cal_F_T_Activa &&
                                                                                    dc.Cal_F_T_Pasiva == sc.Cal_F_T_Pasiva &&
                                                                                    dc.Cal_Liq_Activa == sc.Cal_Liq_Activa &&
                                                                                    dc.Cal_Liq_Pasiva == sc.Cal_Liq_Pasiva &&
                                                                                    dc.Colateral == sc.Colateral &&
                                                                                    dc.Conv_Int_Act == sc.Conv_Int_Act &&
                                                                                    dc.Conv_Int_Pas == sc.Conv_Int_Pas &&
                                                                                    dc.Estructural == sc.Estructural &&
                                                                                    dc.F_Inicio == sc.F_Inicio &&
                                                                                    dc.F_Valuacion == sc.F_Valuacion &&
                                                                                    dc.F_Vencimiento == sc.F_Vencimiento &&
                                                                                    dc.Id_Banxico == sc.Id_Banxico &&
                                                                                    dc.Id_Contrap == sc.Id_Contrap &&
                                                                                    dc.Intencion == sc.Intencion &&
                                                                                    dc.Inter_F == sc.Inter_F &&
                                                                                    dc.Inter_I == sc.Inter_I &&
                                                                                    dc.Llama_Margen == sc.Llama_Margen &&
                                                                                    this.ConvertToDecimal(dc.St_Activa.ToString()!) == this.ConvertToDecimal(sc.St_Activa.ToString()!) &&
                                                                                    this.ConvertToDecimal(dc.St_Pasiva.ToString()!) == this.ConvertToDecimal(sc.St_Pasiva.ToString()!) //&&
                                                                                    //dc.Tc_Activa == sc.Tc_Activa &&
                                                                                    //dc.Tc_Pasiva == sc.Tc_Pasiva
                                                                                 )).ToList();
            }
            else
            {
                newDataList = mirrorDataList;
            }

            if (newDataList.Count == 0)
            {
                respuesta.exito = true;
                respuesta.mensaje = "Los registros de la tabla espejo ya se encuentran almacenados en producción. Registros insertados = 0.";
                return respuesta;
            }

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            //OracleTransaction transaction = conexion.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            //cmd.Transaction = transaction;
            int affectedRecords = 0;
            OracleTransaction? transaction = null;

            try
            {
                conexion.Open();
                transaction = conexion.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                cmd.Transaction = transaction;

                foreach (PosicionPrimariaSwaps item in newDataList)
                {

                    string newTC_Activa = item.Tc_Activa + "%";
                    string newTC_Pasiva = item.Tc_Pasiva + "%";
                    int cte = 0;

                    if (!string.IsNullOrEmpty(item.Tc_Activa) && !char.IsDigit(item.Tc_Activa[0]))
                    {
                        if (item.D_Ante_Activa == null)
                        {
                            newTC_Activa = item.Tc_Activa + "[" + cte + "]";
                        }
                        else
                        {
                            newTC_Activa = item.Tc_Activa + "[" + item.D_Ante_Activa + "]";
                        }
                    }

                    if (!string.IsNullOrEmpty(item.Tc_Pasiva) && !char.IsDigit(item.Tc_Pasiva[0]))
                    {
                        if (item.D_Ante_Pasiva == null)
                        {
                            newTC_Pasiva = item.Tc_Pasiva + "[" + cte + "]";
                        }
                        else
                        {
                            newTC_Pasiva = item.Tc_Pasiva + "[" + item.D_Ante_Pasiva + "]";
                        }
                    }


                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("TIPOPOS", item.Tipopos);
                    cmd.Parameters.Add("FECHAREG", newFechareg);
                    cmd.Parameters.Add("NOMPOS", item.Nompos);
                    //cmd.Parameters.Add("HORAREG", DateTime.UtcNow.ToString("HH:mm:ss"));
                    cmd.Parameters.Add("HORAREG", "00000");
                    cmd.Parameters.Add("CPOSICION", item.CPosicion);
                    cmd.Parameters.Add("COPERACION", item.COperacion);
                    cmd.Parameters.Add("FINICIO", item.F_Inicio);
                    cmd.Parameters.Add("FVENCIMIENTO", item.F_Vencimiento);
                    cmd.Parameters.Add("INTER_I", item.Inter_I);
                    cmd.Parameters.Add("INTER_F", item.Inter_F);
                    cmd.Parameters.Add("AC_INT_ACT", item.Ac_Int_Act);
                    cmd.Parameters.Add("AC_INT_PAS", item.Ac_Int_Pas);
                    cmd.Parameters.Add("TC_ACTIVA", newTC_Activa);
                    cmd.Parameters.Add("TC_PASIVA", newTC_Pasiva);
                    cmd.Parameters.Add("ST_ACTIVA", item.St_Activa);
                    cmd.Parameters.Add("ST_PASIVA", item.St_Pasiva);
                    cmd.Parameters.Add("CONV_INT_ACT", item.Conv_Int_Act);
                    cmd.Parameters.Add("CONV_INT_PAS", item.Conv_Int_Pas);
                    cmd.Parameters.Add("C_PRODUCTO", item.C_Producto);
                    cmd.Parameters.Add("F_VALUACION", item.F_Valuacion);
                    cmd.Parameters.Add("ID_CONTRAP", item.Id_Contrap);
                    cmd.Parameters.Add("ID_BANXICO", item.Id_Banxico);
                    cmd.Parameters.Add("LLAMA_MARGEN", item.Llama_Margen);
                    cmd.Parameters.Add("INTENCION", item.Intencion);
                    cmd.Parameters.Add("ESTRUCTURAL", item.Estructural);
                    cmd.Parameters.Add("CAL_F_T_ACTIVA", item.Cal_F_T_Activa);
                    cmd.Parameters.Add("CAL_F_T_PASIVA", item.Cal_F_T_Pasiva);
                    cmd.Parameters.Add("CAL_LIQ_ACTIVA", item.Cal_Liq_Activa);
                    cmd.Parameters.Add("CAL_LIQ_PASIVA", item.Cal_Liq_Pasiva);
                    cmd.Parameters.Add("PX_SWAP", item.Px_Swap);
                    cmd.Parameters.Add("COLATERAL", item.Colateral);
                    cmd.BindByName = true;

                    cmd.CommandText = "INSERT INTO RIESVARM.VAR_TD_POS_SWAPS (TIPOPOS" +
                                                                                ",FECHAREG" +
                                                                                ",NOMPOS" +
                                                                                ",HORAREG" +
                                                                                ",CPOSICION" +
                                                                                ",COPERACION" +
                                                                                ",FINICIO" +
                                                                                ",FVENCIMIENTO" +
                                                                                ",INTER_I" +
                                                                                ",INTER_F" +
                                                                                ",AC_INT_ACT" +
                                                                                ",AC_INT_PAS" +
                                                                                ",TC_ACTIVA" +
                                                                                ",TC_PASIVA" +
                                                                                ",ST_ACTIVA" +
                                                                                ",ST_PASIVA" +
                                                                                ",CONV_INT_ACT" +
                                                                                ",CONV_INT_PAS" +
                                                                                ",C_PRODUCTO" +
                                                                                ",F_VALUACION" +
                                                                                ",ID_CONTRAP" +
                                                                                ",ID_BANXICO" +
                                                                                ",LLAMA_MARGEN" +
                                                                                ",INTENCION" +
                                                                                ",ESTRUCTURAL" +
                                                                                ",CAL_F_T_ACTIVA " +
                                                                                ",CAL_F_T_PASIVA" +
                                                                                ",CAL_LIQ_ACTIVA" +
                                                                                ",CAL_LIQ_PASIVA" +
                                                                                ",PX_SWAP" +
                                                                                ",COLATERAL) " +
                                                                                "VALUES (:TIPOPOS,to_date(:FECHAREG,'yyyymmdd'),:NOMPOS,:HORAREG,:CPOSICION,:COPERACION,to_date(:FINICIO,'yyyymmdd'),to_date(:FVENCIMIENTO,'yyyymmdd'),:INTER_I,:INTER_F,:AC_INT_ACT,:AC_INT_PAS,:TC_ACTIVA,:TC_PASIVA,:ST_ACTIVA,:ST_PASIVA,:CONV_INT_ACT,:CONV_INT_PAS,:C_PRODUCTO,:F_VALUACION,:ID_CONTRAP,:ID_BANXICO,:LLAMA_MARGEN,:INTENCION,:ESTRUCTURAL,:CAL_F_T_ACTIVA,:CAL_F_T_PASIVA,:CAL_LIQ_ACTIVA,:CAL_LIQ_PASIVA,:PX_SWAP,:COLATERAL)";
                    cmd.CommandType = CommandType.Text;
                    affectedRecords += cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                respuesta.exito = true;
                respuesta.mensaje = affectedRecords + " registros fueron insertados.";
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                respuesta.mensaje = $"{affectedRecords} registros fueron revertidos. " + "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        /// <summary>
        /// Reporte # 18 - Eiminar registros de acuerdo a una fecha especificada
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        private Respuesta eliminarPosicionPrimariasSwaps18(int fechaConsulta, OracleConnection conexion)
        {
            Respuesta response = new Respuesta { exito = false };

            try
            {
                string strSQL = String.Format("DELETE FROM RIESVARM.VAR_TD_POS_SWAPS WHERE FECHAREG = to_date(:FECHAREG,'yyyymmdd')", fechaConsulta);
                //string strSQL = String.Format("DELETE FROM RIESVARM.VAR_TD_POS_SWAPS_BK WHERE FECHAREG = to_date(:FECHAREG,'yyyymmdd')", fechaConsulta);
                using (OracleCommand cmd = new OracleCommand(strSQL, conexion))
                {
                    conexion.Open();

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.BindByName = true;
                    cmd.Parameters.Add("FECHAREG", fechaConsulta);

                    int affectedRecords = cmd.ExecuteNonQuery();

                    response.exito = affectedRecords >= 0;
                }
            }
            catch (Exception ex)
            {
                response.exito = false;
                response.mensaje = ex.Message;
            }
            finally
            {
                conexion.Close();
            }

            return response;
        }

        /// <summary>
        /// Reporte # 18 - Obtener lista de registros en tabla productiva
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        private async Task<List<PosicionPrimariaSwaps>> GetProductionData18(string fechaConsulta, OracleConnection conexion)
        {
            List<PosicionPrimariaSwaps> productionDataList = new List<PosicionPrimariaSwaps>();

            try
            {
                string strSQL = String.Format("SELECT * FROM RIESVARM.VAR_TD_POS_SWAPS WHERE FECHAREG = to_date(:FECHAREG,'yyyymmdd')", fechaConsulta);
                //string strSQL = String.Format("SELECT * FROM RIESVARM.VAR_TD_POS_SWAPS_BK WHERE FECHAREG = to_date(:FECHAREG,'yyyymmdd')", fechaConsulta);
                using (OracleCommand cmd = new OracleCommand(strSQL, conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add("FECHAREG", fechaConsulta!);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.BindByName = true;

                    var reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        //foreach (var item in reader.Cast<DbDataRecord>())
                        //{
                        //    var value = item.GetDecimal("St_Activa");
                        //}


                        while (reader.Read())
                        {
                            productionDataList.Add(new PosicionPrimariaSwaps
                            {
                                Ac_Int_Act = reader.GetString("Ac_Int_Act"),
                                Ac_Int_Pas = reader.GetString("Ac_Int_Pas"),
                                C_Producto = this.ConvertToStr(reader["C_Producto"].ToString()!),
                                Cal_F_T_Activa = reader.GetString("Cal_F_T_Activa"),
                                Cal_F_T_Pasiva = reader.GetString("Cal_F_T_Pasiva"),
                                Cal_Liq_Activa = reader.GetString("Cal_Liq_Activa"),
                                Cal_Liq_Pasiva = reader.GetString("Cal_Liq_Pasiva"),
                                Colateral = reader.GetString("Colateral"),
                                Conv_Int_Act = reader.GetString("Conv_Int_Act"),
                                Conv_Int_Pas = reader.GetString("Conv_Int_Pas"),
                                COperacion = reader.GetString("COperacion"),
                                CPosicion = this.ConvertToInt(reader["CPosicion"].ToString()!),
                                //D_Ante_Activa = reader.GetInt32("D_Ante_Activa"),
                                //D_Ante_Pasiva = reader.GetInt32("D_Ante_Pasiva"),
                                Estructural = reader.GetString("Estructural"),
                                Fechareg = this.ConvertToDate(reader["FechaReg"].ToString()!, 1),
                                F_Inicio = this.ConvertToDate(reader["FInicio"].ToString()!, 1),
                                F_Valuacion = reader.GetString("F_Valuacion"),
                                F_Vencimiento = this.ConvertToDate(reader["FVencimiento"].ToString()!, 1),
                                Horareg = reader.GetString("Horareg"),
                                Id_Banxico = reader.GetString("Id_Banxico"),
                                Id_Contrap = reader.GetString("Id_Contrap"),
                                Intencion = reader.GetString("Intencion"),
                                Inter_F = reader.GetString("Inter_F"),
                                Inter_I = reader.GetString("Inter_I"),
                                Llama_Margen = reader.GetString("Llama_Margen"),
                                Nompos = reader.GetString("Nompos"),
                                Px_Swap = reader.GetString("Px_Swap"),
                                St_Activa = this.ConvertToDecimal(reader["St_Activa"].ToString()!),
                                St_Pasiva = this.ConvertToDecimal(reader["St_Pasiva"].ToString()!),
                                Tc_Activa = reader.GetString("Tc_Activa"),
                                Tc_Pasiva = reader.GetString("Tc_Pasiva"),
                                Tipopos = this.ConvertToInt(reader["TipoPos"].ToString()!)
                            }); ;
                        }
                        reader.Close();
                        await reader.DisposeAsync();
                    }

                    IEnumerable<PosicionPrimariaSwaps> typeData = reader
                                                                    .Cast<IDataRecord>()
                                                                    .Select(dr => new PosicionPrimariaSwaps {
                                                                        Ac_Int_Act = (string)dr["Ac_Int_Act"],
                                                                        Ac_Int_Pas = (string)dr["Ac_Int_Pas"],
                                                                        C_Producto = (string)dr["C_Producto"],
                                                                        Cal_F_T_Activa = (string)dr["Cal_F_T_Activa"],
                                                                        Cal_F_T_Pasiva = (string)dr["Cal_F_T_Pasiva"],
                                                                        Cal_Liq_Activa = (string)dr["Cal_Liq_Activa"],
                                                                        Cal_Liq_Pasiva = (string)dr["Cal_Liq_Pasiva"],
                                                                        Colateral = (string)dr["Colateral"],
                                                                        Conv_Int_Act = (string)dr["Conv_Int_Act"],
                                                                        Conv_Int_Pas = (string)dr["Conv_Int_Pas"],
                                                                        COperacion = (string)dr["COperacion"],
                                                                        CPosicion = (int)dr["CPosicion"],
                                                                        //D_Ante_Activa = (int)dr["D_Ante_Activa"],
                                                                        //D_Ante_Pasiva = (int)dr["D_Ante_Pasiva"],
                                                                        Estructural = (string)dr["Estructural"],
                                                                        Fechareg = (int)dr["Fechareg"],
                                                                        F_Inicio = (int)dr["FInicio"],
                                                                        F_Valuacion = (string)dr["F_Valuacion"],
                                                                        F_Vencimiento = (int)dr["FVencimiento"],
                                                                        Horareg = (string)dr["Horareg"],
                                                                        Id_Banxico = (string)dr["Id_Banxico"],
                                                                        Id_Contrap = (string)dr["Id_Contrap"],
                                                                        Intencion = (string)dr["Intencion"],
                                                                        Inter_F = (string)dr["Inter_F"],
                                                                        Inter_I = (string)dr["Inter_I"],
                                                                        Llama_Margen = (string)dr["Llama_Margen"],
                                                                        Nompos = (string)dr["Nompos"],
                                                                        Px_Swap = (string)dr["Px_Swap"],
                                                                        St_Activa = (decimal)dr["St_Activa"],
                                                                        St_Pasiva = (decimal)dr["St_Pasiva"],
                                                                        Tc_Activa = (string)dr["Tc_Activa"],
                                                                        Tc_Pasiva = (string)dr["Tc_Pasiva"],
                                                                        Tipopos = (int)dr["Tipopos"],
                                                                    });
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
            finally
            {
                conexion.Close();
            }

            return productionDataList;
        }

        #endregion


        #region Reporte # 19


        //REPORTE 19
        public Respuesta guardarReporteOperacionCVDivisas(List<ReporteOperacionCVDivisas> reporte, List<CatalogoDivisas> catalogoDivisas, List<CatalogoPosiciones> catalogoPosiciones, OracleConnection conexionRiesgos)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarReporteOperacionCVDivisas(Convert.ToInt32(reporte[0].Fecha), conexionRiesgos);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexionRiesgos;

            try
            {
                if (borro)
                {
                    conexionRiesgos.Open();
                    int aff = 0;
                    foreach (ReporteOperacionCVDivisas item in reporte)
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FECHA", item.Fecha);
                        cmd.Parameters.Add("CVE_CONTRAPARTE", item.Cve_contraparte);
                        cmd.Parameters.Add("DESCRIPCION", item.Descripcion);
                        cmd.Parameters.Add("TIPO_OPER", item.Tipo_oper);
                        cmd.Parameters.Add("POSICION", catalogoPosiciones.FirstOrDefault(x => x.Id_Apesa == item.Posicion)!.Id_Sivarmer);
                        cmd.Parameters.Add("MONTO", item.Monto);
                        cmd.Parameters.Add("TIPO_MONEDA", item.Tipo_moneda);
                        cmd.Parameters.Add("CVE_MONEDA", catalogoDivisas.FirstOrDefault(x => x.Id_Apesa == item.Tipo_moneda)!.Id_Sivarmer);
                        cmd.Parameters.Add("TIPO_CAMBIO_CONC", item.Tipo_cambio_conc);
                        cmd.Parameters.Add("TIPO_CAMBIO_MDO", item.Tipo_cambio_mdo);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESGO.LIQ_MDO_CAMBIOS (FECHA" +
                                                   ",CVE_CONTRAPARTE" +
                                                   ",DESCRIPCION" +
                                                   ",TIPO_OPER" +
                                                   ",POSICION" +
                                                   ",MONTO" +
                                                   ",TIPO_MONEDA" +
                                                   ",CVE_MONEDA" +
                                                   ",TIPO_CAMBIO_CONC" +
                                                   ",TIPO_CAMBIO_MDO) " +
                                                   "VALUES (to_date(:FECHA,'yyyymmdd'),:CVE_CONTRAPARTE,:DESCRIPCION,:TIPO_OPER,:POSICION,:MONTO,:TIPO_MONEDA,:CVE_MONEDA,:TIPO_CAMBIO_CONC,:TIPO_CAMBIO_MDO)";

                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.exito = false;
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexionRiesgos.Close();
            }
            return respuesta;
        }


        /// <summary>
        /// Reporte # 19
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public bool eliminarReporteOperacionCVDivisas(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHA", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESGO.LIQ_MDO_CAMBIOS WHERE Fecha = to_date(:FECHA,'yyyymmdd')", fechaConsulta);


                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        #endregion


        #region Reporte # 20

        //REPORTE 20
        public Respuesta guardarPosicionesPrimForwards(List<PosicionesPrimForwards> reporte, List<CatalogoDivisas> catalogoDivisas, OracleConnection conexionRiesgos)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarPosicionesPrimForwards(Convert.ToInt32(reporte[0].FPosicion), conexionRiesgos);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexionRiesgos;

            try
            {
                if (borro)
                {
                    conexionRiesgos.Open();
                    int aff = 0;
                    foreach (PosicionesPrimForwards item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("F_POSICION", item.FPosicion);
                        cmd.Parameters.Add("CIKOS", item.CIkos);
                        cmd.Parameters.Add("F_INICIO", item.F_inicio);
                        cmd.Parameters.Add("F_VEN", item.F_ven);
                        cmd.Parameters.Add("F_LIQ", item.F_Liq);
                        cmd.Parameters.Add("MONTO_BASE_ACT", item.Monto_Base_Act);
                        cmd.Parameters.Add("MONTO_FWD_ACT", item.Monto_Fwd_Act);
                        cmd.Parameters.Add("MONEDA_ACT", catalogoDivisas.FirstOrDefault(x => x.Id_Apesa == item.Moneda_Act)!.Id_Sivarmer);
                        cmd.Parameters.Add("CURVA_VAL_ACTIVO", item.Curva_Val_Activo);
                        cmd.Parameters.Add("MONTO_BASE_PAS", item.Monto_Base_Pas);
                        cmd.Parameters.Add("MONTO_FWD_PAS", item.Monto_Fwd_Pas);
                        cmd.Parameters.Add("MONEDA_PAS", catalogoDivisas.FirstOrDefault(x => x.Id_Apesa == item.Moneda_Pas)!.Id_Sivarmer);
                        cmd.Parameters.Add("CURVA_VAL_PASIVO", item.Curva_Val_Pasivo);

                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.IKOS_POSPRIM_FORWARDS (F_POSICION" +
                                                   ",CIKOS" +
                                                   ",F_INICIO" +
                                                   ",F_VEN" +
                                                   ",F_LIQ" +
                                                   ",MONTO_BASE_ACT" +
                                                   ",MONTO_FWD_ACT" +
                                                   ",MONEDA_ACT" +
                                                   ",CURVA_VAL_ACTIVO" +
                                                   ",MONTO_BASE_PAS" +
                                                   ",MONTO_FWD_PAS" +
                                                   ",MONEDA_PAS" +
                                                   ",CURVA_VAL_PASIVO) " +
                                                   "VALUES (TO_DATE(:F_POSICION,'yyyymmdd'),:CIKOS,TO_DATE(:F_INICIO,'yyyymmdd'),TO_DATE(:F_VEN,'yyyymmdd'),TO_DATE(:F_LIQ,'yyyymmdd'),:MONTO_BASE_ACT,:MONTO_FWD_ACT,:MONEDA_ACT,:CURVA_VAL_ACTIVO,:MONTO_BASE_PAS,:MONTO_FWD_PAS,:MONEDA_PAS,:CURVA_VAL_PASIVO)";

                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.exito = false;
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexionRiesgos.Close();
            }
            return respuesta;
        }

        /// <summary>
        /// Reporte # 20
        /// </summary>
        /// <param name="fechaConsulta"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public bool eliminarPosicionesPrimForwards(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("F_POSICION", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.IKOS_POSPRIM_FORWARDS WHERE F_POSICION =to_date(:F_POSICION,'yyyymmdd')", fechaConsulta);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        #endregion


        #region Funciones de Conversión

        /// <summary>
        /// Convertir el valor de una fila de un objeto DataReader en cadena
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private string ConvertToStr(string strValue) 
        {
            string valueConverted = string.Empty;

            if (!string.IsNullOrEmpty(strValue))
            {
                valueConverted = strValue;
            }

            return valueConverted;
        }

        /// <summary>
        /// Convertir el valor de una fila de un objeto DataReader en fecha
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private int ConvertToDate(string strValue, int type)
        {
            int valueConverted = 0;

            if (!string.IsNullOrEmpty(strValue))
            {
                switch (type)
                {
                    case 1:
                        //De la forma => 01/04/2024 12:00:00 a. m.
                        valueConverted = int.Parse(DateTime.Parse(strValue).ToString("yyyyMMdd"));
                        break;
                    case 2:
                        //De la forma => 20240401
                        valueConverted = int.Parse(DateTime.ParseExact(strValue, "yyyyMMdd", null).ToString("yyyyMMdd"));
                        break;
                }
            }

            return valueConverted;
        }

        /// <summary>
        /// Convertir el valor de una fila de un objeto DataReader en entero
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private int ConvertToInt(string strValue) 
        {
            int valueConverted = 0;

            if (!string.IsNullOrEmpty(strValue))
            {
                valueConverted = int.Parse(strValue);
            }

            return valueConverted;
        }

        /// <summary>
        /// Convertir el valor de una fila de un objeto DataReader en decimal
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private decimal? ConvertToDecimal(string strValue)
        {
            decimal? valueConverted = null;

            try
            {
                if (!string.IsNullOrEmpty(strValue))
                {
                    valueConverted = decimal.Parse(strValue);
                }
            }
            catch (Exception)
            {
                valueConverted = null;
            }

            return valueConverted;
        }

        #endregion


        #region Reporte Riesgo-Rango

        /// <summary>
        /// Reporte Riesgo-Rango
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexion"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public Respuesta guardarComprasVentasOperador(List<ComprasVentasOperador> reporte, OracleConnection conexion, int fechaIni, int fechaFin)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = this.eliminarComprasVentasOperador(conexion, fechaIni, fechaFin);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (ComprasVentasOperador item in reporte)
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FechaConcertacion", item.FechaConcertacion);
                        cmd.Parameters.Add("Contraparte", item.Contraparte);
                        cmd.Parameters.Add("Contrato", item.Contrato);
                        cmd.Parameters.Add("TipoValor", item.TipoValor);
                        cmd.Parameters.Add("Emisor", item.Emisor);
                        cmd.Parameters.Add("Serie", item.Serie);
                        cmd.Parameters.Add("ClaveOper", item.ClaveOper);
                        cmd.Parameters.Add("TipoOper", item.TipoOper);
                        cmd.Parameters.Add("ImporteAsignado", item.ImporteAsignado);
                        cmd.Parameters.Add("ImporteCierre", item.ImporteCierre);
                        cmd.Parameters.Add("Parte", item.Parte);
                        cmd.Parameters.Add("Usuario", item.Usuario);
                        cmd.BindByName = true;

                        cmd.CommandText = "INSERT INTO RIESVARM.COMPRAS_VENTAS_OPER (FechaConcertacion" +
                                                    ",Contraparte" +
                                                    ",Contrato" +
                                                    ",TipoValor" +
                                                    ",Emisor" +
                                                    ",Serie" +
                                                    ",ClaveOper" +
                                                    ",TipoOper" +
                                                    ",ImporteAsignado" +
                                                    ",ImporteCierre" +
                                                    ",Parte" +
                                                    ",Usuario)" +
                                                    "VALUES (:FechaConcertacion, :Contraparte, :Contrato, :TipoValor, :Emisor, :Serie, :ClaveOper, :TipoOper, :ImporteAsignado, :ImporteCierre, :Parte, :Usuario)";

                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";
                }
                else
                {
                    respuesta.mensaje = "Hubo un error al borrar los registros.";
                }
            }
            catch (Exception ex)
            {
                respuesta.exito = false;
                respuesta.mensaje = "Hubo un error al insertar los registros. " + ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }


        /// <summary>
        /// Reporte Riesgo-Rango
        /// </summary>
        /// <param name="conexion"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        private bool eliminarComprasVentasOperador(OracleConnection conexion, int fechaIni, int fechaFin)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FechaConcertacion", fechaIni);
                cmd.Parameters.Add("Contraparte", fechaFin);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.COMPRAS_VENTAS_OPER WHERE FechaConcertacion >= :FechaConcertacion AND FechaConcertacion <= :Contraparte", fechaIni, fechaFin);

                aff = cmd.ExecuteNonQuery();

                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;
        }

        #endregion


        public List<CatalogoDivisas> GetCatalogoDivisas(string esquema, OracleConnection conexion)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            List<CatalogoDivisas> result = new List<CatalogoDivisas>();
            try
            {
                conexion.Open();
                cmd.CommandText = $"SELECT * FROM {esquema}.CATALOGO_DIVISAS ";

                var reader = cmd.ExecuteReader();
                result = reader.Cast<IDataRecord>().Select(x => new CatalogoDivisas
                {
                    Id_Apesa = (string)x["ID_APESA"],
                    Id_Sivarmer = (decimal)x["ID_SIVARMER"],
                }).ToList();


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return result;
        }


        public List<CatalogoPosiciones> GetCatalogoPosiciones(OracleConnection conexion)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;
            List<CatalogoPosiciones> result = new List<CatalogoPosiciones>();
            try
            {
                conexion.Open();
                cmd.CommandText = "SELECT * FROM RIESVARM.CATALOGO_ID_POSICIONES ";

                var reader = cmd.ExecuteReader();
                result = reader.Cast<IDataRecord>().Select(x => new CatalogoPosiciones
                {
                    Id_Apesa = (string)x["ID_APESA"],
                    Id_Sivarmer = (string)x["ID_SIVARMER"],
                }).ToList();


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return result;
        }
        

    }
}
