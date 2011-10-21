using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EntregaProductoDAL
    {
        //METODOS DE BUSQUEDA
        public static void ObtenerEntregas(int codigoCliente, DataTable dtEntregas)
        {
            string sql = @"SELECT entrega_codigo, entrega_fecha, cli_codigo, e_codigo
                        FROM ENTREGA_PRODUCTO WHERE cli_codigo=@p0";

            object[] param = { codigoCliente };
            try
            {
                DB.FillDataTable(dtEntregas, sql, param);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static void ObtenerEntregas(DateTime fechaEntrega, DataTable dtEntregas)
        {
            string sql = @"SELECT entrega_codigo, entrega_fecha, cli_codigo, e_codigo
                        FROM ENTREGA_PRODUCTO WHERE entrega_fecha >=@p0 and entrega_fecha < dateadd(dd,1, @p0)";

            object[] param = { fechaEntrega.ToString("yyyyMMdd")};
            try
            {
                DB.FillDataTable(dtEntregas, sql, param);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static void ObtenerEntregas(int codigoCliente, DateTime fechaEntrega, DataTable dtEntregas)
        {

            string sql = @"SELECT entrega_codigo, entrega_fecha, cli_codigo, e_codigo
                        FROM ENTREGA_PRODUCTO WHERE cli_codigo=@p0 and entrega_fecha >=@p1 and entrega_fecha < dateadd(dd,1, @p1)";


            object[] param = { codigoCliente, fechaEntrega.ToString("yyyyMMdd") };
            try
            {
                DB.FillDataTable(dtEntregas, sql, param);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerDetalleEntrega(int idEntrega, DataTable dtEntregas)
        {
            string sql = @"SELECT dent_codigo, entrega_codigo,dped_codigo, dent_cantidad, dent_contenido
                        FROM DETALLE_ENTREGA_PRODUCTO WHERE entrega_codigo=@p0";


            object[] param = { idEntrega };
            try
            {
                DB.FillDataTable(dtEntregas, sql, param);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que guarda la entrega del producto
        public static void GuardarEntrega(Entidades.EntregaProducto entrega, Data.dsEntregaProducto dsEntregaProducto)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();

                //Guardamos la cabecera
                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                string sql = "INSERT INTO [ENTREGA_PRODUCTO] ([entrega_fecha], [cli_codigo], [e_codigo]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                object[] valorParametros = { entrega.Fecha, entrega.Cliente.Codigo, entrega.Empleado.Codigo };
                entrega.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, transaccion));

                //Inserto el Detalle
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    if (row.DPED_CODIGO != 0)
                    {
                        sql = "INSERT INTO [DETALLE_ENTREGA_PRODUCTO] ([entrega_codigo], [dped_codigo], [dent_cantidad], [dent_contenido]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
                        object[] valorParam = { entrega.Codigo, row.DPED_CODIGO, row.DENT_CANTIDAD, row.DENT_CONTENIDO };
                        row.BeginEdit();
                        row.DENT_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.ENTREGA_CODIGO = entrega.Codigo;
                        row.EndEdit();
                    }
                    else
                    {
                        sql = "INSERT INTO [DETALLE_ENTREGA_PRODUCTO] ([entrega_codigo], [dent_cantidad], [dent_contenido]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                        object[] valorParam = { entrega.Codigo, row.DENT_CANTIDAD, row.DENT_CONTENIDO };
                        row.BeginEdit();
                        row.DENT_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.ENTREGA_CODIGO = entrega.Codigo;
                        row.EndEdit();

                    }
                }

                transaccion.Commit();
                DB.FinalizarTransaccion();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
        }

        //Metodo que guarda la entrega del producto modificada
        public static void GuardarEntregaModificada(Entidades.EntregaProducto entrega, Data.dsEntregaProducto dsEntregaProducto)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                
                string sql=string.Empty;

                //Guardamos la cabecera
                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                //string sql = "INSERT INTO [ENTREGA_PRODUCTO] ([entrega_fecha], [cli_codigo], [e_codigo]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                //object[] valorParametros = { entrega.Fecha, entrega.Cliente.Codigo, entrega.Empleado.Codigo };
                //entrega.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, transaccion));

                //Inserto al Detalle las filas agregadas
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    if (row.DPED_CODIGO != 0)
                    {
                        sql = "INSERT INTO [DETALLE_ENTREGA_PRODUCTO] ([entrega_codigo], [dped_codigo], [dent_cantidad], [dent_contenido]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
                        object[] valorParam = { entrega.Codigo, row.DPED_CODIGO, row.DENT_CANTIDAD, row.DENT_CONTENIDO };
                        row.BeginEdit();
                        row.DENT_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.ENTREGA_CODIGO = entrega.Codigo;
                        row.EndEdit();
                    }
                    else
                    {
                        sql = "INSERT INTO [DETALLE_ENTREGA_PRODUCTO] ([entrega_codigo], [dent_cantidad], [dent_contenido]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                        object[] valorParam = { entrega.Codigo, row.DENT_CANTIDAD, row.DENT_CONTENIDO };
                        row.BeginEdit();
                        row.DENT_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.ENTREGA_CODIGO = entrega.Codigo;
                        row.EndEdit();

                    }
                }

                //Guardo las modificaciones de las filas
                sql = "UPDATE [DETALLE_ENTREGA_PRODUCTO] SET dent_cantidad=@p0 WHERE dent_codigo=@p1";
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    object[] valorPar = { row.DENT_CANTIDAD, row.DENT_CODIGO };
                    DB.executeNonQuery(sql, valorPar, transaccion);
                }

                //Elimino las que fueron sacadas
                sql = "DELETE FROM [DETALLE_ENTREGA_PRODUCTO] WHERE dent_codigo=@p0 ";
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //pregunto si tiene un pedido asociado le actualizo el estado
                    if (Convert.ToInt32(row["DPED_CODIGO", System.Data.DataRowVersion.Original]) != 0)
                    {
                        //obtengo ese detalle
                        DAL.DetallePedidoDAL.ObtenerUnDetallePedido(dsEntregaProducto.DETALLE_PEDIDOS, Convert.ToInt32(row["DPED_CODIGO", System.Data.DataRowVersion.Original]));

                        foreach (Data.dsEntregaProducto.DETALLE_PEDIDOSRow dped in dsEntregaProducto.DETALLE_PEDIDOS.Rows)
                        {
                            if (Convert.ToInt32(row["DPED_CODIGO", System.Data.DataRowVersion.Original]) == Convert.ToInt32(dped["DPED_CODIGO", System.Data.DataRowVersion.Original]))
                            {
                                //Le actualizo el estado al detalle
                                DAL.DetallePedidoDAL.CambiarEstado(Convert.ToInt32(dped["DPED_CODIGO", System.Data.DataRowVersion.Original]), 2);

                                //Le actualizo el estado del pedido
                                DAL.PedidoDAL.CambiarEstadoPedido(Convert.ToInt32(dped["DPED_CODIGO", System.Data.DataRowVersion.Original]), 2);
                            }
                        }

                    }

                    //Lo borro de la base de datos
                    object[] valorPara = { Convert.ToInt32(row["DPMES_CODIGO", System.Data.DataRowVersion.Original]) };
                    DB.executeNonQuery(sql, valorPara, transaccion);
                }

                transaccion.Commit();
                DB.FinalizarTransaccion();

            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
        }

        //METODO PARA ELIMINAR DATOS DE LA BASE DE DATOS
        public static void EliminarEntrega(int codigoEntrega)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Elimino el detalle del plan mensual
                string sql = "DELETE FROM DETALLE_ENTREGA_PRODUCTO WHERE entrega_codigo = @p0";
                object[] valorParametros = { codigoEntrega };
                DB.executeNonQuery(sql, valorParametros, transaccion);

                //Elimino la demanda
                sql = "DELETE FROM ENTREGA_PRODUCTO WHERE entrega_codigo = @p0";
                DB.executeNonQuery(sql, valorParametros, transaccion);

                transaccion.Commit();
                DB.FinalizarTransaccion();
            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }

        }
        
    }
}
