CREATE DATABASE MSSLeilao
GO

USE MSSLeilao; 
GO

/*******[TABELA PRODUTOS]********/

CREATE TABLE [dbo].[Produtos](
	[ProdutosID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Valor] [decimal] NOT NULL,
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
	[Valor] [decimal] NOT NULL,
 CONSTRAINT [PK_dbo.Lances] PRIMARY KEY CLUSTERED 
(
	[LancesID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lances]  WITH CHECK ADD CONSTRAINT [FK_dbo.Lances_dbo.Pessoas_PessoasID] FOREIGN KEY([PessoasID])
REFERENCES [dbo].[Pessoas] ([PessoasID])
GO

ALTER TABLE [dbo].[Lances] CHECK CONSTRAINT [FK_dbo.Lances_dbo.Pessoas_PessoasID]
GO

ALTER TABLE [dbo].[Lances]  WITH CHECK ADD CONSTRAINT [FK_dbo.Lances_dbo.Produtos_ProdutosID] FOREIGN KEY([ProdutosID])
REFERENCES [dbo].[Produtos] ([ProdutosID])
GO

ALTER TABLE [dbo].[Lances] CHECK CONSTRAINT [FK_dbo.Lances_dbo.Produtos_ProdutosID]
GO