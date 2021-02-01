/*Exemplo de uma consulta dos lances de um produto usando o like*/
SELECT p.Nome, p.Valor [Valor Inicial], pe.Nome FROM Produtos p
JOIN Lances l ON l.ProdutosID = p.ProdutosID
JOIN Pessoas pe ON pe.PessoasID = l.PessoasID
WHERE p.Nome LIKE '%cad%'