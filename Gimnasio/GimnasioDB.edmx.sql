
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/05/2021 14:23:07
-- Generated from EDMX file: C:\Users\Windows\Desktop\Jonathan\Universidad\Proyecto final Laboratirio III\FinalLaboratorio3\Gimnasio\GimnasioDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Gimnasio];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Entrenado__activ__2E1BDC42]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entrenador_Actividad] DROP CONSTRAINT [FK__Entrenado__activ__2E1BDC42];
GO
IF OBJECT_ID(N'[dbo].[FK__Entrenado__entre__2F10007B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entrenador_Actividad] DROP CONSTRAINT [FK__Entrenado__entre__2F10007B];
GO
IF OBJECT_ID(N'[dbo].[FK__Inscripci__activ__37A5467C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inscripcion] DROP CONSTRAINT [FK__Inscripci__activ__37A5467C];
GO
IF OBJECT_ID(N'[dbo].[FK__Inscripci__clien__36B12243]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inscripcion] DROP CONSTRAINT [FK__Inscripci__clien__36B12243];
GO
IF OBJECT_ID(N'[dbo].[FK__Inscripci__plan___38996AB5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inscripcion] DROP CONSTRAINT [FK__Inscripci__plan___38996AB5];
GO
IF OBJECT_ID(N'[dbo].[FK__Registro__client__33D4B598]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Registro] DROP CONSTRAINT [FK__Registro__client__33D4B598];
GO
IF OBJECT_ID(N'[dbo].[FK__Usuario__rol_id__25869641]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK__Usuario__rol_id__25869641];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Actividad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Actividad];
GO
IF OBJECT_ID(N'[dbo].[Cliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cliente];
GO
IF OBJECT_ID(N'[dbo].[Entrenador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entrenador];
GO
IF OBJECT_ID(N'[dbo].[Entrenador_Actividad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entrenador_Actividad];
GO
IF OBJECT_ID(N'[dbo].[Inscripcion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Inscripcion];
GO
IF OBJECT_ID(N'[dbo].[Plaan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Plaan];
GO
IF OBJECT_ID(N'[dbo].[Registro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Registro];
GO
IF OBJECT_ID(N'[dbo].[Rol]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rol];
GO
IF OBJECT_ID(N'[dbo].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuario];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Actividad'
CREATE TABLE [dbo].[Actividad] (
    [actividad_id] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(20)  NOT NULL,
    [tipo] varchar(200)  NOT NULL,
    [borrado_logico] bit  NULL
);
GO

-- Creating table 'Cliente'
CREATE TABLE [dbo].[Cliente] (
    [cliente_id] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(20)  NOT NULL,
    [apellido] varchar(20)  NOT NULL,
    [dni] bigint  NOT NULL,
    [fecha_nac] datetime  NULL,
    [sexo] varchar(20)  NOT NULL,
    [borrado_logico] bit  NULL
);
GO

-- Creating table 'Entrenador'
CREATE TABLE [dbo].[Entrenador] (
    [entrenador_id] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(20)  NOT NULL,
    [apellido] varchar(20)  NOT NULL,
    [dni] int  NOT NULL,
    [fecha] datetime  NOT NULL,
    [sexo] varchar(20)  NULL,
    [titulo] varchar(20)  NULL,
    [borrado_logico] bit  NULL
);
GO

-- Creating table 'Entrenador_Actividad'
CREATE TABLE [dbo].[Entrenador_Actividad] (
    [entre_act_id] int IDENTITY(1,1) NOT NULL,
    [actividad_id] int  NOT NULL,
    [entrenador_id] int  NOT NULL
);
GO

-- Creating table 'Inscripcion'
CREATE TABLE [dbo].[Inscripcion] (
    [inscripcion_id] int IDENTITY(1,1) NOT NULL,
    [cliente_id] int  NOT NULL,
    [actividad_id] int  NOT NULL,
    [plan_id] int  NOT NULL,
    [fecha_inicio] datetime  NOT NULL,
    [cant_dias] int  NULL,
    [activo] bit  NOT NULL,
    [borrado_logico] bit  NULL
);
GO

-- Creating table 'Plaan'
CREATE TABLE [dbo].[Plaan] (
    [plan_id] int IDENTITY(1,1) NOT NULL,
    [cant_dias] int  NOT NULL,
    [fecha_limite] datetime  NULL,
    [borrado_logico] bit  NULL
);
GO

-- Creating table 'Registro'
CREATE TABLE [dbo].[Registro] (
    [registro_id] int IDENTITY(1,1) NOT NULL,
    [dia_ingreso] datetime  NOT NULL,
    [cliente_id] int  NOT NULL,
    [borrado_logico] bit  NULL,
    [hora_ingreso] varchar(40)  NULL
);
GO

-- Creating table 'Rol'
CREATE TABLE [dbo].[Rol] (
    [rol_id] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(20)  NOT NULL,
    [borrado_logico] bit  NULL
);
GO

-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [usuario_id] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(20)  NOT NULL,
    [apellido] varchar(20)  NOT NULL,
    [contraseña] varchar(200)  NULL,
    [dni] bigint  NOT NULL,
    [rol_id] int  NOT NULL,
    [borrado_logico] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [actividad_id] in table 'Actividad'
ALTER TABLE [dbo].[Actividad]
ADD CONSTRAINT [PK_Actividad]
    PRIMARY KEY CLUSTERED ([actividad_id] ASC);
GO

-- Creating primary key on [cliente_id] in table 'Cliente'
ALTER TABLE [dbo].[Cliente]
ADD CONSTRAINT [PK_Cliente]
    PRIMARY KEY CLUSTERED ([cliente_id] ASC);
GO

-- Creating primary key on [entrenador_id] in table 'Entrenador'
ALTER TABLE [dbo].[Entrenador]
ADD CONSTRAINT [PK_Entrenador]
    PRIMARY KEY CLUSTERED ([entrenador_id] ASC);
GO

-- Creating primary key on [entre_act_id] in table 'Entrenador_Actividad'
ALTER TABLE [dbo].[Entrenador_Actividad]
ADD CONSTRAINT [PK_Entrenador_Actividad]
    PRIMARY KEY CLUSTERED ([entre_act_id] ASC);
GO

-- Creating primary key on [inscripcion_id] in table 'Inscripcion'
ALTER TABLE [dbo].[Inscripcion]
ADD CONSTRAINT [PK_Inscripcion]
    PRIMARY KEY CLUSTERED ([inscripcion_id] ASC);
GO

-- Creating primary key on [plan_id] in table 'Plaan'
ALTER TABLE [dbo].[Plaan]
ADD CONSTRAINT [PK_Plaan]
    PRIMARY KEY CLUSTERED ([plan_id] ASC);
GO

-- Creating primary key on [registro_id] in table 'Registro'
ALTER TABLE [dbo].[Registro]
ADD CONSTRAINT [PK_Registro]
    PRIMARY KEY CLUSTERED ([registro_id] ASC);
GO

-- Creating primary key on [rol_id] in table 'Rol'
ALTER TABLE [dbo].[Rol]
ADD CONSTRAINT [PK_Rol]
    PRIMARY KEY CLUSTERED ([rol_id] ASC);
GO

-- Creating primary key on [usuario_id] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([usuario_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [actividad_id] in table 'Entrenador_Actividad'
ALTER TABLE [dbo].[Entrenador_Actividad]
ADD CONSTRAINT [FK__Entrenado__activ__2E1BDC42]
    FOREIGN KEY ([actividad_id])
    REFERENCES [dbo].[Actividad]
        ([actividad_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Entrenado__activ__2E1BDC42'
CREATE INDEX [IX_FK__Entrenado__activ__2E1BDC42]
ON [dbo].[Entrenador_Actividad]
    ([actividad_id]);
GO

-- Creating foreign key on [actividad_id] in table 'Inscripcion'
ALTER TABLE [dbo].[Inscripcion]
ADD CONSTRAINT [FK__Inscripci__activ__37A5467C]
    FOREIGN KEY ([actividad_id])
    REFERENCES [dbo].[Actividad]
        ([actividad_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Inscripci__activ__37A5467C'
CREATE INDEX [IX_FK__Inscripci__activ__37A5467C]
ON [dbo].[Inscripcion]
    ([actividad_id]);
GO

-- Creating foreign key on [cliente_id] in table 'Inscripcion'
ALTER TABLE [dbo].[Inscripcion]
ADD CONSTRAINT [FK__Inscripci__clien__36B12243]
    FOREIGN KEY ([cliente_id])
    REFERENCES [dbo].[Cliente]
        ([cliente_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Inscripci__clien__36B12243'
CREATE INDEX [IX_FK__Inscripci__clien__36B12243]
ON [dbo].[Inscripcion]
    ([cliente_id]);
GO

-- Creating foreign key on [cliente_id] in table 'Registro'
ALTER TABLE [dbo].[Registro]
ADD CONSTRAINT [FK__Registro__client__33D4B598]
    FOREIGN KEY ([cliente_id])
    REFERENCES [dbo].[Cliente]
        ([cliente_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Registro__client__33D4B598'
CREATE INDEX [IX_FK__Registro__client__33D4B598]
ON [dbo].[Registro]
    ([cliente_id]);
GO

-- Creating foreign key on [entrenador_id] in table 'Entrenador_Actividad'
ALTER TABLE [dbo].[Entrenador_Actividad]
ADD CONSTRAINT [FK__Entrenado__entre__2F10007B]
    FOREIGN KEY ([entrenador_id])
    REFERENCES [dbo].[Entrenador]
        ([entrenador_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Entrenado__entre__2F10007B'
CREATE INDEX [IX_FK__Entrenado__entre__2F10007B]
ON [dbo].[Entrenador_Actividad]
    ([entrenador_id]);
GO

-- Creating foreign key on [plan_id] in table 'Inscripcion'
ALTER TABLE [dbo].[Inscripcion]
ADD CONSTRAINT [FK__Inscripci__plan___38996AB5]
    FOREIGN KEY ([plan_id])
    REFERENCES [dbo].[Plaan]
        ([plan_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Inscripci__plan___38996AB5'
CREATE INDEX [IX_FK__Inscripci__plan___38996AB5]
ON [dbo].[Inscripcion]
    ([plan_id]);
GO

-- Creating foreign key on [rol_id] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK__Usuario__rol_id__25869641]
    FOREIGN KEY ([rol_id])
    REFERENCES [dbo].[Rol]
        ([rol_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Usuario__rol_id__25869641'
CREATE INDEX [IX_FK__Usuario__rol_id__25869641]
ON [dbo].[Usuario]
    ([rol_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------