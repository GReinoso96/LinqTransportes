using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LinqTransportes.DAO
{
	public class DevelDAO
	{
		private SqlConnection con;

		public DevelDAO()
		{
			Conexion();
		}

		public bool Conexion()
		{
			Console.WriteLine(Statics.Texto.StrConexionAbrir);
			con = DB.Database.GetConnection();

			if (con != null)
				Console.WriteLine(Statics.Texto.StrConexionEstablecida);
			else
				throw new Exception(Statics.Texto.StrConexionError);
			return true;
		}

		public void Dispose()
		{
			con.Dispose();
			con.Close();
			Console.WriteLine(Statics.Texto.StrConexionCerrar);
		}

		public void CreateDB()
		{
			if (con != null)
			{
				CreateClientes();
				CreateTrabajadores();
				CreateBodegas();
				CreateOrdenes();
				CreateCamiones();
			}
		}

		public void ClearDB()
		{
			using (SqlCommand sql = new SqlCommand(@" 
						DROP TABLE Bodegas;
						DROP TABLE CamionChofer;
						DROP TABLE Camiones;
						DROP TABLE Clientes;
						DROP TABLE Ordenes;
						DROP TABLE Puestos;
						DROP TABLE Regiones;
						DROP TABLE Trabajadores;
						", con))
			{
				sql.ExecuteNonQuery();
			}
		}

		private void CreateClientes()
		{
			using (SqlCommand sql = new SqlCommand(@" 
						IF object_id('Clientes', 'U') is null
						BEGIN
						CREATE TABLE [dbo].[Clientes] (
							[Id]            INT          IDENTITY (1, 1) NOT NULL,
							[Nombres]       VARCHAR (50) NOT NULL,
							[APaterno]      VARCHAR (50) NOT NULL,
							[AMaterno]      VARCHAR (50) NOT NULL,
							[Rut]           VARCHAR (50) NOT NULL,
							[Telefono]      VARCHAR (50) NULL,
							[Email]         VARCHAR (50) NOT NULL,
							[Direccion]     VARCHAR (50) NOT NULL,
							[FechaCreacion] DATETIME     NOT NULL,
							PRIMARY KEY CLUSTERED ([Id] ASC)
						);
						INSERT INTO [dbo].[Clientes] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, FechaCreacion)
						VALUES ('Francisco Eduardo', 'Guillermo', 'Froilan', '11867459-6', '+569 99334455', 'fguillermo@correo.cl', 'Francia 5020, Santiago', GETDATE());
						INSERT INTO [dbo].[Clientes] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, FechaCreacion)
						VALUES ('Carlos Andres', 'Villagran', 'Tapia', '13875485-5', '+569 86122352', 'cavillagran@correo.cl', 'Eiffel 721, Arica', GETDATE());
						END", con))

				sql.ExecuteNonQuery();
		}

		private void CreateCamiones()
		{
			using (SqlCommand sql = new SqlCommand(@" 
						IF object_id('Camiones', 'U') is null
						BEGIN
						CREATE TABLE [dbo].[Camiones] (
							[Id]            INT          IDENTITY (1, 1) NOT NULL,
							[Patente]       VARCHAR (50) NOT NULL,
							[Peso]			VARCHAR (50) NOT NULL,
							[Volumen]		VARCHAR (50) NOT NULL,
							PRIMARY KEY CLUSTERED ([Id] ASC)
						);
						CREATE TABLE [dbo].[CamionChofer] (
							[Id]            INT          IDENTITY (1, 1) NOT NULL,
							[Camion]		VARCHAR (50) NOT NULL,
							[Chofer]		VARCHAR (50) NOT NULL,
							PRIMARY KEY CLUSTERED ([Id] ASC)
						);
						INSERT INTO [dbo].[Camiones] (Patente, Peso, Volumen) VALUES ('DLVK 22', 25000, 28);
						INSERT INTO [dbo].[Camiones] (Patente, Peso, Volumen) VALUES ('HKLM 18', 30000, 30);
						INSERT INTO [dbo].[Camiones] (Patente, Peso, Volumen) VALUES ('UVDE 55', 25000, 28);
						INSERT INTO [dbo].[Camiones] (Patente, Peso, Volumen) VALUES ('CLPA 73', 30000, 30);
						INSERT INTO [dbo].[Camiones] (Patente, Peso, Volumen) VALUES ('ASPA 91', 25000, 28);
						INSERT INTO [dbo].[CamionChofer] (Camion, Chofer) VALUES (1, 1);
						INSERT INTO [dbo].[CamionChofer] (Camion, Chofer) VALUES (2, 2);
						INSERT INTO [dbo].[CamionChofer] (Camion, Chofer) VALUES (3, 3);
						INSERT INTO [dbo].[CamionChofer] (Camion, Chofer) VALUES (4, 4);
						INSERT INTO [dbo].[CamionChofer] (Camion, Chofer) VALUES (5, 5);
						END", con))

				sql.ExecuteNonQuery();
		}

		private void CreateOrdenes()
		{
			using (SqlCommand sql = new SqlCommand(@" 
						IF object_id('Ordenes', 'U') is null
						BEGIN
						CREATE TABLE [dbo].[Ordenes] (
							[Id]            INT          IDENTITY (1, 1) NOT NULL,
							[Numero]		VARCHAR (50) NOT NULL,
							[Remitente]		VARCHAR (50) NOT NULL,
							[Destinatario]	VARCHAR (50) NOT NULL,
							[Bodega]		VARCHAR (50) NOT NULL,
							[Fecha]			VARCHAR (50) NULL,
							[Total]			VARCHAR (50) NOT NULL,
							PRIMARY KEY CLUSTERED ([Id] ASC)
						); END", con))

				sql.ExecuteNonQuery();
		}

		private void CreateTrabajadores()
		{
			using (SqlCommand sql = new SqlCommand(@" 
						IF object_id('Trabajadores', 'U') is null
						BEGIN
						CREATE TABLE [dbo].[Trabajadores] (
							[Id]            INT          IDENTITY (1, 1) NOT NULL,
							[Nombres]       VARCHAR (50) NOT NULL,
							[APaterno]      VARCHAR (50) NOT NULL,
							[AMaterno]      VARCHAR (50) NOT NULL,
							[Rut]           VARCHAR (50) NOT NULL,
							[Telefono]      VARCHAR (50) NULL,
							[Email]         VARCHAR (50) NOT NULL,
							[Direccion]     VARCHAR (50) NOT NULL,
							[Contrasena]    VARCHAR (500) NOT NULL,
							[FechaCreacion] DATETIME     NOT NULL,
							[Puesto]        INT          NOT NULL,
							PRIMARY KEY CLUSTERED ([Id] ASC)
						);
						CREATE TABLE [dbo].[Puestos] (
							[Id]            INT          IDENTITY (1, 1) NOT NULL,
							[Nombre]		VARCHAR (50) NOT NULL
							PRIMARY KEY CLUSTERED ([Id] ASC)
						);
						INSERT INTO [dbo].[Puestos] (Nombre) VALUES ('Recepcionista');
						INSERT INTO [dbo].[Puestos] (Nombre) VALUES ('Chofer');
						INSERT INTO [dbo].[Puestos] (Nombre) VALUES ('Administrador');

						INSERT INTO [dbo].[Trabajadores] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, Puesto, FechaCreacion, Contrasena)
						VALUES ('Marco Antonio', 'Villamar', 'Andrade', '10821268-3', '+569 15456238', 'mantonio@correo.cl', 'Bernardo OHiggings 154, Pedro Aguirre Cerda', 2, GETDATE(), '-');
						INSERT INTO [dbo].[Trabajadores] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, Puesto, FechaCreacion, Contrasena)
						VALUES ('Francisco Alonso', 'Tapia', 'Tapia', '13514040-6', '+569 78941235', 'ftapia@correo.cl', 'Caupolican 458, El Bosque', 2, GETDATE(), '-');
						INSERT INTO [dbo].[Trabajadores] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, Puesto, FechaCreacion, Contrasena)
						VALUES ('Alejandro', 'Perez', 'Espinoza', '18283966-3', '+569 78649634', 'aperez@correo.cl', 'Encalada 214, La Granja', 2, GETDATE(), '-');
						INSERT INTO [dbo].[Trabajadores] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, Puesto, FechaCreacion, Contrasena)
						VALUES ('Antonio Cruz', 'Castillo', 'Gonzales', '13534313-7', '+569 74562318', 'agonzales@correo.cl', 'Coronel 179, Lampa', 2, GETDATE(), '-');
						INSERT INTO [dbo].[Trabajadores] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, Puesto, FechaCreacion, Contrasena)
						VALUES ('Segundo Ariel', 'Torres', 'Escalona', '9394126-8', '+569 74562318', 'storres@correo.cl', 'Militares 1853, Conchalí', 2, GETDATE(), '-');

						INSERT INTO [dbo].[Trabajadores] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, Puesto, FechaCreacion, Contrasena)
						VALUES ('Enrique', 'Jofre', 'Rojas', '19444137-1', '+569 86122352', 'ejofre@correo.cl', 'Cumming 1025, Santiago', 1, GETDATE(), '-');
						INSERT INTO [dbo].[Trabajadores] (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, Puesto, FechaCreacion, Contrasena)
						VALUES ('Jeremias', 'Santana', 'Velmar', '12673165-5', '+569 41562894', 'jsantana@correo.cl', 'Paris 75, Lo Barnechea', 3, GETDATE(), '-');
						END", con))

				sql.ExecuteNonQuery();
		}

		private void CreateBodegas()
		{
			using (SqlCommand sql = new SqlCommand(@" 
						IF object_id('Bodegas', 'U') is null
						BEGIN
						CREATE TABLE [dbo].[Bodegas] (
							[Id]            INT          IDENTITY (1, 1) NOT NULL,
							[Region]		VARCHAR (50) NOT NULL,
							[Direccion]     VARCHAR (50) NOT NULL,
							PRIMARY KEY CLUSTERED ([Id] ASC)
						);
						CREATE TABLE [dbo].[Regiones] (
							[Id]            INT          IDENTITY (1, 1) NOT NULL,
							[Nombre]       VARCHAR (50) NOT NULL
							PRIMARY KEY CLUSTERED ([Id] ASC)
						);
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Arica y Parinacota');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Tarapacá');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Antofagasta');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Atacama');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Coquimbo');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Valparaíso');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Metropolitana');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Libertador Bernardo OHiggins');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Maule');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Ñuble');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Biobío');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Araucanía');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Los Ríos');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Los Lagos');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Aysen');
						INSERT INTO [dbo].[Regiones] (Nombre) VALUES ('Magallanes y Antartica');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (1, 'Bodega Arica');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (2, 'Bodega Iquique');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (3, 'Bodega Antofagasta');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (4, 'Bodega Copiapó');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (5, 'Bodega Serena Coquimbo');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (6, 'Bodega Valparaiso');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (7, 'Bodega Est Central');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (8, 'Bodega Rancagua');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (9, 'Bodega Talca');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (10, 'Bodega Chillán');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (11, 'Bodega Concepcion');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (12, 'Bodega Temuco');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (13, 'Bodega Valdivia');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (14, 'Bodega Puerto Montt');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (15, 'Bodega Coyhaique');
						INSERT INTO [dbo].[Bodegas] (Region, Direccion) VALUES (16, 'Bodega Punta Arenas');
						END", con))

				sql.ExecuteNonQuery();
		}
	}
}