
# Santo Sabor - Back-end


## Rodando o Projeto

Clone o projeto: 

**por HTTPS**

```bash
  https://github.com/matheuskormann/Back-endSantoSabor.git
```
  **por SSH**
```bash
  git@github.com:matheuskormann/Back-endSantoSabor.git
```

## Configure o banco de dados local
No arquivo appsettings.json dentro da pasta santosabor.presentation, altere a string de conexão para apontar para seu SQL server local

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SantoSaborDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
## Caso utilize autenticação por usuário e senha, ajuste para:

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SantoSaborDB;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
}
```

## Restaure os pacotes e atualize o banco 
O MyDbContext está localizado na pasta Infrastructure. Para aplicar as migrations e atualizar o banco rode os comandos abaixo na raiz do projeto (SantoSaborBackend ou o nome que você colocou)

Criar migration (substitua "NomeDaMigration" por um nome que descreva as mudanças)
```bash
dotnet ef migrations add NomeDaMigration --project SantoSabor.Infrastructure --startup-project SantoSabor.Presentation
```

```bash
# Atualizar o banco aplicando a migration
dotnet ef database update --project SantoSabor.Infrastructure --startup-project SantoSabor.Presentation
```

## Se não possui a ferramenta dotnet -ef instalada: 

```bash
dotnet tool install --global dotnet-ef
```

# Iniciando o servidor

## Entre no diretório do projeto

```bash
  cd SantoSabor.Presentation
```

## Execute o comando 
```bash
  dotnet run
```

## Após iniciar o projeto, acesse a interface Swagger através do seguinte link:

📌 **Nota:** Substitua `PORTA` pela porta que aparece no terminal ao rodar a aplicação.
```bash
   http://localhost:PORTA/swagger/index.html
```


## Autores

- [@Lucas Baumer](https://www.github.com/lucasbaumer)
- [@Matheus Kormann](https://www.github.com/matheuskormann)


