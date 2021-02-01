CREATE DATABASE MSSLeilao
GO

USE MSSLeilao; 
GO

/*******[TABELA PRODUTOS]********/

CREATE TABLE [dbo].[Produtos](
	[ProdutosID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Valor] [decimal](18,2) NOT NULL,
 CONSTRAINT [PK_dbo.Produtos] PRIMARY KEY CLUSTERED 
(
	[ProdutosID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO

/*******[TABELA PESSOAS]********/

CREATE TABLE [dbo].[Pessoas](
	[PessoasID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Idade] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pessoas] PRIMARY KEY CLUSTERED 
(
	[PessoasID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO

/*******[TABELA LANCES]********/

CREATE TABLE [dbo].[Lances](
	[LancesID] [int] IDENTITY(1,1) NOT NULL,
	[PessoasID] [int] NOT NULL,
	[ProdutosID] [int] NOT NULL,
	[Valor] [decimal](18,2) NOT NULL,
 CONSTRAINT [PK_dbo.Lances] PRIMARY KEY CLUSTERED 
(
	[LancesID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lances]  WITH CHECK ADD CONSTRAINT [FK_dbo.Lances_dbo.Pessoas_PessoasID] FOREIGN KEY([PessoasID])
REFERENCES [dbo].[Pessoas] ([PessoasID]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Lances] CHECK CONSTRAINT [FK_dbo.Lances_dbo.Pessoas_PessoasID]
GO

ALTER TABLE [dbo].[Lances]  WITH CHECK ADD CONSTRAINT [FK_dbo.Lances_dbo.Produtos_ProdutosID] FOREIGN KEY([ProdutosID])
REFERENCES [dbo].[Produtos] ([ProdutosID]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Lances] CHECK CONSTRAINT [FK_dbo.Lances_dbo.Produtos_ProdutosID]
GO


/*Adiconar alguns produtos...pessoas...lances...para simulação da consulta*/
INSERT INTO [dbo].[Pessoas] 
VALUES ('Matheus', 22), ('Carlos', 23), ('Rodrigo', 35);
GO

INSERT INTO [dbo].[Produtos] 
VALUES ('Disco', 13.95), ('Celular', 899.99), ('Geladeira', 1200);
GO

INSERT INTO [dbo].[Lances] 
VALUES (1, 2, 899.99), (1, 2, 919.99), (1, 2, 1099.99), (1, 2, 1199.99);
GO

SELECT p.Nome, p.Valor [Valor Inicial], pe.Nome FROM Produtos p
JOIN Lances l ON l.ProdutosID = p.ProdutosID
JOIN Pessoas pe ON pe.PessoasID = l.PessoasID
WHERE p.Nome LIKE '%cel%'
