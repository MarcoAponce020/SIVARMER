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

namespace DAO
{
    public class DAORiesgos
    {
        private const string Value = "FechaValor";

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

            borro = eliminarComprasVentasOperador(conexion, fechaIni, fechaFin);

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
        public Respuesta guardarReporteMDCambios(List<ReporteMDCambios> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarReporteMDCambios(Convert.ToInt32(reporte[0].Fecha), conexionRiesgos);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexionRiesgos;

            try
            {
                if (borro)
                {
                    conexionRiesgos.Open();
                    int aff = 0;
                    foreach (ReporteMDCambios item in reporte)
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("Fecha", item.Fecha);
                        cmd.Parameters.Add("Cve_contraparte", item.Cve_contraparte);
                        cmd.Parameters.Add("Descripcion", item.Descripcion);
                        cmd.Parameters.Add("Tipo_oper", item.Tipo_oper);
                        cmd.Parameters.Add("Posicion", item.Posicion);
                        cmd.Parameters.Add("Monto", item.Monto);
                        cmd.Parameters.Add("Tipo_moneda", item.Tipo_moneda);
                        cmd.Parameters.Add("Cve_moneda", item.Cve_moneda);
                        cmd.Parameters.Add("Tipo_cambio_conc", item.Tipo_cambio_conc);
                        cmd.Parameters.Add("Tipo_cambio_mdo", item.Tipo_cambio_mdo);

                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESGO.LIQ_MDO_CAMBIOS; (Fecha" +
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
                conexionRiesgos.Close();
            }
            return respuesta;
        }
        public Respuesta guardarFlujosSwaps(List<FlujosSwaps> reporte, OracleConnection conexion)
        {
            if (reporte is null)
            {
                throw new ArgumentNullException(nameof(reporte));
            }

            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarFlujoswaps(Convert.ToInt32(reporte[0].FECHA_POS), conexion);

            conexion.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;


            try
            {
                if (borro)
                {
                    //conexion.Open();
                    int aff = 0;
                    foreach (FlujosSwaps item in reporte)
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FECHA_POS", item.FECHA_POS);
                        cmd.Parameters.Add("CVESWAP", item.CVESWAP);
                        cmd.Parameters.Add("POSICION", item.POSICION);
                        cmd.Parameters.Add("SECUENCIA", item.SECUENCIA);
                        cmd.Parameters.Add("TIP_NEGOC", item.TIP_NEGOC);
                        cmd.Parameters.Add("FEC_INI", item.FEC_INI);
                        cmd.Parameters.Add("FEC_TER", item.FEC_TER);
                        cmd.Parameters.Add("FEC_INI_R", item.FEC_INI_R);
                        cmd.Parameters.Add("FEC_TER_R", item.FEC_TER_R);
                        cmd.Parameters.Add("VALOR_NOC", item.VALOR_NOC);
                        cmd.Parameters.Add("AMORTIZACION", item.AMORTIZACION);
                        cmd.Parameters.Add("TASA", item.TASA);
                        cmd.Parameters.Add("SPREAD", item.SPREAD);
                        cmd.Parameters.Add("CUPON", item.CUPON);
                        cmd.Parameters.Add("CONVENCION", item.CONVENCION);
                        cmd.Parameters.Add("NUMMON", item.NUMMON);
                        cmd.Parameters.Add("TIP_SWAP", item.TIP_SWAP);
                        cmd.Parameters.Add("ID_SIDE", item.ID_SIDE);
                        cmd.Parameters.Add("INTERI", item.INTERI);
                        cmd.Parameters.Add("INTERF", item.INTERF);
                        cmd.Parameters.Add("FEC_LIQ", item.FEC_LIQ);
                        cmd.Parameters.Add("NEGO_ESTRUC", item.NEGO_ESTRUC);
                        cmd.Parameters.Add("PAGA_INT", item.PAGA_INT);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO XYZWIN.SW_RIESGOS1 (FECHA_POS" +
                                                                                     ",CVSWAP" +
                                                                                    ",POSICION" +
                                                                                    ",SECUENCIA" +
                                                                                    ",TIP_NEGOC" +
                                                                                    ",FEC_INI" +
                                                                                    ",FEC_TER" +
                                                                                    ",FEC_INI_R" +
                                                                                    ",FEC_TER_R" + 
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
                                                                                      "VALUES (:FECHA_POS,:CVSWAP,:POSICION,:SECUENCIA,:TIP_NEGOC,:FEC_INI,:FEC_TER,:FEC_INI_R,:FEC_TER_R,:VALOR_NOC,:AMORTIZACION,:TASA,:SPREAD,:CUPON,:NUMMON,:TIP_SWAP,:ID_SIDE,:INTERI,:INTERF,:FEC_LIQ,:NEGO_ESTRUC,:PAGA_INT)";
                        cmd.CommandType = CommandType.Text;
                        aff += cmd.ExecuteNonQuery();
                    }

                    respuesta.exito = true;
                    respuesta.mensaje = aff + " registros fueron insertados.";

                    bool comparacion = false;
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
        public Respuesta guardarCaracteristicasSwaps(List<CaracteristicasSwaps> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarCaracteristicasSwaps(Convert.ToInt32(reporte[0].FECHA_POS), conexion);

            conexion.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;



            try
            {
                if (borro)
                {
                    //conexion.Open();
                    int aff = 0;
                    foreach (CaracteristicasSwaps item in reporte)
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FECHA_POS", item.FECHA_POS);
                        cmd.Parameters.Add("PERIODO", item.PERIODO);
                        cmd.Parameters.Add("CVE_INST", item.CVE_INST);
                        cmd.Parameters.Add("CVE_PROV", item.CVE_PROV);
                        cmd.Parameters.Add("NUMSEC", item.NUMSEC);
                        cmd.Parameters.Add("CVECONTPP", item.CVECONTPP);
                        cmd.Parameters.Add("FEC_OPERAC", item.FEC_OPERAC);
                        cmd.Parameters.Add("FEC_INI", item.FEC_INI);
                        cmd.Parameters.Add("FEC_VENC", item.FEC_VENC);
                        cmd.Parameters.Add("TIPO_VALOR", item.TIPO_VALOR);
                        cmd.Parameters.Add("TIPO_OPER", item.TIPO_OPER);
                        cmd.Parameters.Add("FEC_PROX_FLUJO", item.FEC_PROX_FLUJO);
                        cmd.Parameters.Add("TASA_RECIBE", item.TASA_RECIBE);
                        cmd.Parameters.Add("PERIODO_FLUJO", item.PERIODO_FLUJO);
                        cmd.Parameters.Add("BASE_CAL_TASA", item.BASE_CAL_TASA);
                        cmd.Parameters.Add("CVEMONEDA", item.CVEMONEDA);
                        cmd.Parameters.Add("TIPO_CAMBIO_REC", item.TIPO_CAMBIO_REC);
                        cmd.Parameters.Add("M_NOC_FLUJO", item.M_NOC_FLUJO);
                        cmd.Parameters.Add("FORMULA_FLUJO", item.FORMULA_FLUJO);
                        cmd.Parameters.Add("F_PROX_FLUJO_ENT", item.F_PROX_FLUJO_ENT);
                        cmd.Parameters.Add("PERIODICIDAD", item.PERIODICIDAD);
                        cmd.Parameters.Add("BASE_CALCULO", item.BASE_CALCULO);
                        cmd.Parameters.Add("TIPO_MONEDA", item.TIPO_MONEDA);
                        cmd.Parameters.Add("TIPO_CAMBIO_ENT", item.TIPO_CAMBIO_ENT);
                        cmd.Parameters.Add("M_NOC_FLUJO_ENT", item.M_NOC_FLUJO_ENT);
                        cmd.Parameters.Add("TASA_REF_ACTIVA", item.TASA_REF_ACTIVA);
                        cmd.Parameters.Add("T_REF_ACTIVA_DIAS_ANTE", item.T_REF_ACTIVA_DIAS_ANTE);
                        cmd.Parameters.Add("TASA_REF_PASIVA", item.TASA_REF_PASIVA);
                        cmd.Parameters.Add("T_REF_PASIVA_DIAS_ANTE", item.T_REF_PASIVA_DIAS_ANTE);
                        cmd.Parameters.Add("SOBRETASA_ACTIVA", item.SOBRETASA_ACTIVA);
                        cmd.Parameters.Add("OP_SOBRETASA_ACTIVA", item.OP_SOBRETASA_ACTIVA);
                        cmd.Parameters.Add("SOBRETASA_PASIVA", item.SOBRETASA_PASIVA);
                        cmd.Parameters.Add("OP_SOBRETASA_PASIVA", item.OP_SOBRETASA_PASIVA);
                        cmd.Parameters.Add("TASA_ENTREGA", item.TASA_ENTREGA);
                        cmd.Parameters.Add("PRIMA", item.PRIMA);
                        cmd.Parameters.Add("OBJETIVO_OPER", item.OBJETIVO_OPER);
                        cmd.Parameters.Add("VAL_ACTIVA", item.VAL_ACTIVA);
                        cmd.Parameters.Add("VAL_PASIVA", item.VAL_PASIVA);
                        cmd.Parameters.Add("VALOR_NETO", item.VALOR_NETO);
                        cmd.Parameters.Add("MARCA_MERCADO", item.MARCA_MERCADO);
                        cmd.Parameters.Add("ID_SIDE", item.ID_SIDE);
                        cmd.Parameters.Add("LLAMA_MARGEN", item.LLAMA_MARGEN);
                        cmd.Parameters.Add("NEGO_ESTRUC", item.NEGO_ESTRUC);
                        cmd.Parameters.Add("REINVIERTE_INT_ACT", item.REINVIERTE_INT_ACT);
                        cmd.Parameters.Add("REINVIERTE_INT_PAS", item.REINVIERTE_INT_PAS);


                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO XYZWIN.SW_RIESGOS2 (FECHA_POS" +
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
                                                                           ",TASA_RECIBE" +
                                                                           ",PERIODO_FLUJO" +
                                                                           ",BASE_CAL_TASA" +
                                                                           ",CVEMONEDA" +
                                                                           ",TIPO_CAMBIO_REC" +
                                                                           ",M_NOC_FLUJO" +
                                                                           ",FORMULA_FLUJO" +
                                                                           ",F_PROX_FLUJO_ENT" +
                                                                           ",PERIODICIDAD" +
                                                                           ",BASE_CALCULO" +
                                                                           ",TIPO_MONEDA" +
                                                                           ",TIPO_CAMBIO_ENT" +
                                                                           ",M_NOC_FLUJO_ENT" +
                                                                           ",TASA_REF_ACTIVA" +
                                                                           ",T_REF_ACTIVA_DIAS_ANTE" +
                                                                           ",TASA_REF_PASIVA" +
                                                                           ",T_REF_PASIVA_DIAS_ANTE" +
                                                                           ",SOBRETASA_ACTIVA" +
                                                                           ",OP_SOBRETASA_ACTIVA" +
                                                                           ",SOBRETASA_PASIVA" +
                                                                           ",OP_SOBRETASA_PASIVA" +
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
                                                                           ",REINVIERTE_INT_PAS) " +
                                                                            "VALUES (:FECHA_POS,:PERIODO,:CVE_INST,:CVE_PROV,:NUMSEC,:CVECONTPP,:FEC_OPERAC,:FEC_INI,:FEC_VENC,:TIPO_VALOR,:TIPO_OPER,:FEC_PROX_FLUJO,:TASA_RECIBE,:PERIODO_FLUJO,:BASE_CAL_TASA,:CVEMONEDA,:TIPO_CAMBIO_REC,:M_NOC_FLUJO,:FORMULA_FLUJO,:F_PROX_FLUJO_ENT,:PERIODICIDAD,:BASE_CALCULO,:TIPO_MONEDA,:TIPO_CAMBIO_ENT,:M_NOC_FLUJO_ENT,:TASA_REF_ACTIVA,:T_REF_ACTIVA_DIAS_ANTE,:TASA_REF_PASIVA,:T_REF_PASIVA_DIAS_ANTE,:SOBRETASA_ACTIVA,:OP_SOBRETASA_ACTIVA,:SOBRETASA_PASIVA,:OP_SOBRETASA_PASIVA,:TASA_ENTREGA,:PRIMA,:OBJETIVO_OPER,:VAL_ACTIVA,:VAL_PASIVA,:VALOR_NETO,:MARCA_MERCADO,:ID_SIDE,:LLAMA_MARGEN,:NEGO_ESTRUC,:REINVIERTE_INT_ACT,:REINVIERTE_INT_PAS)";
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
        public Respuesta guardarPosicionPrimariaSwaps(List<PosicionPrimariaSwaps> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;

            }

            borro = eliminarPosicionPrimariasSwaps(Convert.ToInt32(reporte[0].FECHAREG), conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (PosicionPrimariaSwaps item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("TIPOPOS", item.TIPOPOS);
                        cmd.Parameters.Add("FECHAREG", item.FECHAREG);
                        cmd.Parameters.Add("NOMPOS", item.NOMPOS);
                        cmd.Parameters.Add("HORAREG", item.HORAREG);
                        cmd.Parameters.Add("CPOSICION", item.CPOSICION);
                        cmd.Parameters.Add("COPERACION", item.COPERACION);
                        cmd.Parameters.Add("FINICIO", item.FINICIO);
                        cmd.Parameters.Add("FVENCIMIENTO", item.FVENCIMIENTO);
                        cmd.Parameters.Add("INTER_I", item.INTER_I);
                        cmd.Parameters.Add("INTER_F", item.INTER_F);
                        cmd.Parameters.Add("AC_INT_ACT", item.AC_INT_ACT);
                        cmd.Parameters.Add("AC_INT_PAS", item.AC_INT_PAS);
                        cmd.Parameters.Add("TC_ACTIVA", item.TC_ACTIVA);
                        cmd.Parameters.Add("TC_PASIVA", item.TC_PASIVA);
                        cmd.Parameters.Add("D_ANTE D_ANTE_ACTIVA", item.D_ANTE_ACTIVA);
                        cmd.Parameters.Add("CONV_INT_ACT", item.CONV_INT_ACT);
                        cmd.Parameters.Add("CONV_INT_PAS", item.CONV_INT_PAS);
                        cmd.Parameters.Add("C_PRODUCTO", item.C_PRODUCTO);
                        cmd.Parameters.Add("F_VALUACION", item.F_VALUACION);
                        cmd.Parameters.Add("ID_CONTRAP", item.ID_CONTRAP);
                        cmd.Parameters.Add("ID_BANXICO", item.ID_BANXICO);
                        cmd.Parameters.Add("LLAMA_MARGEN", item.LLAMA_MARGEN);
                        cmd.Parameters.Add("INTENCION", item.INTENCION);
                        cmd.Parameters.Add("ESTRUCTURAL", item.ESTRUCTURAL);
                        cmd.Parameters.Add("CAL_F_T_ACTIVA", item.CAL_F_T_ACTIVA);
                        cmd.Parameters.Add("CAL_F_T_PASIVA", item.CAL_F_T_PASIVA);
                        cmd.Parameters.Add("CAL_LIQ_ACTIVA", item.CAL_LIQ_ACTIVA);
                        cmd.Parameters.Add("CAL_LIQ_PASIVA", item.CAL_LIQ_PASIVA);
                        cmd.Parameters.Add("PX_SWAP", item.PX_SWAP);
                        cmd.Parameters.Add("COLATERAL", item.COLATERAL);
                        cmd.Parameters.Add("REF_POS_COB", item.REF_POS_COB);
                        cmd.Parameters.Add("TIPO_POS_COB", item.TIPO_POS_COB);
                        cmd.Parameters.Add("PORC_COB", item.PORC_COB);
                        cmd.Parameters.Add("D_ANTE D_ANTE_PASIVA", item.D_ANTE_PASIVA);
                        cmd.Parameters.Add("OP_ST_ACTIVA", item.OP_ST_ACTIVA);
                        cmd.Parameters.Add("OP_ST_PASIVA", item.OP_ST_PASIVA);
                        cmd.Parameters.Add("ST_ACTIVA", item.ST_ACTIVA);
                        cmd.Parameters.Add("ST_PASIVA", item.ST_PASIVA);
                        cmd.Parameters.Add("MONEDA_ACT", item.MONEDA_ACT);
                        cmd.Parameters.Add("MONEDA_PAS", item.MONEDA_PAS);
                        cmd.BindByName = true;
                        cmd.CommandText = "INSERT INTO RIESVARM.VAR_TD_POS_SWAPS_BK (TIPOPOS" +
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
                                                                                               ",D_ANTE_ACTIVA" +
                                                                                               ",CONV_INT_ACT" +
                                                                                               ", CONV_INT_PAS" +
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
                                                                                              ",PX_SWAP" +
                                                                                              ",COLATERAL" +
                                                                                              ",REF_POS_COB" +
                                                                                              ",TIPO_POS_COB" +
                                                                                              ",PORC_COB" +
                                                                                              ",D_ANTE_PASIVA" +
                                                                                              ",OP_ST_ACTIVA" +
                                                                                              ",OP_ST_PASIVA" +
                                                                                              ",ST_ACTIVA" +
                                                                                              ",ST_PASIVA" +
                                                                                              ",MONEDA_ACT" +
                                                                                              ",MONEDA_PAS) " +
                                                                                        "VALUES (TIPOPOS,:FECHAREG,:NOMPOS,:HORAREG,:CPOSICION,:COPERACION,:FINICIO,:FVENCIMIENTO,:INTER_I,:INTER_F,:AC_INT_ACT,:AC_INT_PAS,:TC_ACTIVA,:TC_PASIVA,:D_ANTE_ACTIVA,:CONV_INT_ACT,: CONV_INT_PAS,:C_PRODUCTO,:F_VALUACION,:ID_CONTRAP,:ID_BANXICO,:LLAMA_MARGEN,:INTENCION,:ESTRUCTURAL,:CAL_F_T_ACTIVA ,:CAL_F_T_PASIVA,:CAL_LIQ_ACTIVA,:PX_SWAP,:COLATERAL,:REF_POS_COB,:TIPO_POS_COB,:PORC_COB,:D_ANTE_PASIVA,:OP_ST_ACTIVA,:OP_ST_PASIVA,:ST_ACTIVA,:ST_PASIVA,:MONEDA_ACT,:MONEDA_PAS)";
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
        public Respuesta guardarFlujosPosicionesPrimarias(List<FlujosPosicionesPrimarias> reporte, OracleConnection conexion)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            bool borro = false;

            if (reporte.Count <= 0)
            {
                respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                return respuesta;
            }

            borro = eliminarFlujosPosicionesPrimarias(Convert.ToInt32(reporte[0].FECHAREG), conexion);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                if (borro)
                {
                    conexion.Open();
                    int aff = 0;
                    foreach (FlujosPosicionesPrimarias item in reporte)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("TIPOPOS ", item.TIPOPOS);
                        cmd.Parameters.Add("FECHAREG ", item.FECHAREG);
                        cmd.Parameters.Add("NOMPOS", item.NOMPOS);
                        cmd.Parameters.Add("HORAREG ", item.HORAREG);
                        cmd.Parameters.Add("CPOSICION ", item.CPOSICION);
                        cmd.Parameters.Add("COPERACION ", item.COPERACION);
                        cmd.Parameters.Add("T_PATA", item.T_PATA);
                        cmd.Parameters.Add("F_FIXING ", item.F_FIXING);
                        cmd.Parameters.Add("F_INICIO ", item.F_INICIO);
                        cmd.Parameters.Add("F_FINAL ", item.F_FINAL);
                        cmd.Parameters.Add("F_DESC", item.F_DESC);
                        cmd.Parameters.Add("PAGO_INT ", item.PAGO_INT);
                        cmd.Parameters.Add("INT_S_SALDO ", item.INT_S_SALDO);
                        cmd.Parameters.Add("SALDO", item.SALDO);
                        cmd.Parameters.Add("AMORTIZACION ", item.AMORTIZACION);
                        cmd.Parameters.Add("T_APLICAR ", item.T_APLICAR);
                        cmd.BindByName = true;
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
                                                                                       "VALUES (:TIPOPOS,:FECHAREG,:NOMPOS,:HORAREG,:CPOSICION,:COPERACION,:T_PATA,:F_FIXING,:F_INICIO,:F_FINAL,:F_DESC,:INT_S_SALDO,:SALDO,:AMORTIZACION,:T_APLICAR)";

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
        public bool eliminarComprasVentasOperador(OracleConnection conexion, int fechaIni, int fechaFin)
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
        public bool eliminarReporteMDCambios(int fechaConsulta, OracleConnection conexion)
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
                cmd.CommandText = String.Format("DELETE FROM RIESGO.LIQ_MDO_CAMBIOS WHERE Fecha = :Fecha", fechaConsulta);

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
                cmd.CommandText = String.Format("DELETE FROM XYZWIN.SW_RIESGOS1 WHERE FECHA_POS = :FECHA_POS", fechaConsulta);

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
                cmd.CommandText = String.Format("DELETE FROM XYZWIN.SW_RIESGOS2 WHERE FECHA_POS = :FECHA_POS", fechaConsulta);

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
        public bool eliminarPosicionPrimariasSwaps(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHAREG", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.VAR_TD_POS_SWAPS_BK WHERE FECHAREG = :FECHAREG", fechaConsulta);

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
        public bool eliminarFlujosPosicionesPrimarias(int fechaConsulta, OracleConnection conexion)
        {
            bool exito = false;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conexion;

            try
            {
                conexion.Open();
                int aff = 0;
                cmd.Parameters.Add("FECHAREG", fechaConsulta);
                cmd.BindByName = true;
                cmd.CommandText = String.Format("DELETE FROM RIESVARM.VAR_TD_FLUJOS_SWAPS_BK WHERE FECHAREG = :FECHAREG", fechaConsulta);

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
    }
}
