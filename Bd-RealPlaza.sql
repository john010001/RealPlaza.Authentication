USE [RL_SEGURIDAD]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 19/02/2024 20:36:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Correo] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[FechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_GuardarUsuarioAppRealPlaza]    Script Date: 19/02/2024 20:36:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GuardarUsuarioAppRealPlaza]
    
    @CORREO VARCHAR(250),
    @PASSWORD VARCHAR(100),
	@FECHAREGISTRO DATETIME
AS
BEGIN
    INSERT INTO dbo.Usuario
    (
        Correo,
        [Password],
        FechaRegistro
		
    )
    VALUES
    (@CORREO, @PASSWORD,@FECHAREGISTRO);
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerUsuarioCorreo]    Script Date: 19/02/2024 20:36:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[sp_ObtenerUsuarioCorreo]
(
	@Correo VARCHAR(50)
)
AS	
SELECT TOP 1 *, Usu.[PassWord] From  Usuario Usu
WHERE Correo = @Correo 
GO
