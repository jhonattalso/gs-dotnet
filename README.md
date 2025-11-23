# BalanceMe Academy

O projeto **BalanceMe Academy** é o módulo corporativo web de educação e bem-estar da solução "BalanceMe". O aplicativo mobile diagnostica o problema com os dados do usuário (ex: usuário cansado, estressado, ou trabalhando demais). E a plataforma web entra como solução. 

O projeto BalanceMe Academy consiste na implementação de uma plataforma Web corporativa desenvolvida em ASP.NET Core MVC, voltada para a gestão de conteúdo de reskilling e upskilling. É um portal onde o RH da empresa disponibiliza conteúdo para ajudar os colaboradores a desenvolverem as "competências humanas", ou dicas de saúde e bem-estar.   

### Integrantes do Grupo
* **Jhonatta Lima Sandes de Oliveira** - RM: 560277
* **Rangel Bernadi Jordão** - RM: 560547
* **Lucas José Lima** - RM: 561160

**Turma:** 2TDSPA

---

## Decisões Arquiteturais

O projeto foi construído seguindo a arquitetura **MVC (Model-View-Controller)** utilizando **ASP.NET Core**, garantindo a separação de responsabilidades entre a interface do usuário, a lógica de negócios e o acesso a dados.

* **Framework:** .NET 9 / ASP.NET Core MVC.
* **ORM:** Entity Framework Core (EF Core) para manipulação de dados.
* **Banco de Dados:** Oracle Database.
* **Design Patterns:**
    * **Dependency Injection:** Utilizada para injetar dependências (como `ContentService` e `IContentRepository`) através dos construtores, promovendo baixo acoplamento.
    * **Service Layer:** Camada (`ContentService.cs`) responsável pelas regras de negócios (ex: validação de data de publicação), evitando que os Controladores fiquem sobrecarregados.
    * **Repository Pattern:** Camada (`IContentRepository`) responsável exclusivamente pelo acesso a dados e consultas ao banco, isolando o Entity Framework do restante da aplicação.
* **Front-end:** Razor Views com Bootstrap para estilização responsiva e layout consistente.
* **Validações:** Data Annotations (`[Required]`, `[StringLength]`) nos Models para garantir a integridade dos dados antes da persistência.

---

## Rotas e Navegação (Endpoints)

A aplicação utiliza **Attribute Routing** para personalizar as URLs, tornando-as mais descritivas (ex: `/academy` em vez de `/Contents`), além de rotas padrão para a área administrativa.

| Funcionalidade | Método HTTP | Rota / Endpoint | Descrição | Acesso |
| :--- | :---: | :--- | :--- | :---: |
| **Home (Redirecionamento)** | `GET` | `/` | Redireciona automaticamente para a listagem principal (`/academy`). | Público |
| **Listar Conteúdos** | `GET` | `/academy` | Página principal. Exibe cards de conteúdos, filtros (busca, categoria, nível) e paginação. | Público |
| **Detalhes do Conteúdo** | `GET` | `/academy/details/{id}` | Exibe o artigo completo, resumo, vídeo e imagem de capa de um conteúdo específico. | Público |
| **Login Administrativo** | `GET` | `/Admin/Login` | Exibe o formulário de login para administradores. | Público |
| **Autenticar Admin** | `POST` | `/Admin/Login` | Processa as credenciais (Usuário: `Admin` / Senha: `Admin123@`). | Público |
| **Logout** | `GET` | `/Admin/Logout` | Encerra a sessão do administrador e redireciona para a Home. | Admin |
| **Criar Conteúdo** | `GET` | `/academy/create` | Exibe o formulário de cadastro de novo conteúdo. | **Admin** |
| **Salvar Conteúdo** | `POST` | `/academy/create` | Processa a inclusão do novo registro no banco de dados. | **Admin** |
| **Editar Conteúdo** | `GET` | `/academy/edit/{id}` | Exibe o formulário de edição carregado com os dados atuais do conteúdo. | **Admin** |
| **Atualizar Conteúdo** | `POST` | `/academy/edit/{id}` | Processa as alterações realizadas no conteúdo. | **Admin** |
| **Confirmar Exclusão** | `GET` | `/academy/delete/{id}` | Exibe os detalhes do conteúdo para confirmação antes de apagar. | **Admin** |
| **Excluir Conteúdo** | `POST` | `/academy/delete/{id}` | Remove definitivamente o registro do banco de dados. | **Admin** |

> **Nota:** As rotas marcadas com acesso **Admin** verificam a sessão do usuário (`IsAdmin`) e redirecionam para a tela de login caso não esteja autenticado.


## Exemplos de Uso (Fluxos Principais)

### 1. Área Pública - "Academy"
O acesso principal é feito pela rota `/academy`. Nesta área, qualquer utilizador pode visualizar os conteúdos disponíveis.

<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/ee7ce671-8294-4f5c-9934-6c56166e492f" />




* **Visualização de Detalhes:**
    * Ao clicar no botão **"Ler Artigo"** num card, o utilizador é direcionado para `/academy/details/{id}`.
    * Esta página exibe o artigo completo (`ArticleBody`), a imagem de capa e o vídeo do YouTube incorporado (se houver `MediaUrl`).
<img width="1897" height="913" alt="image" src="https://github.com/user-attachments/assets/7bd5386f-2cd1-48d0-a808-acab916b0bc9" />


---

### 2. Área Administrativa - Gestão de Conteúdo
Para adicionar, editar ou remover conteúdos, é necessário estar autenticado como Administrador.

* **Login de Administrador:**
    * Aceda a `/Admin/Login`.
    * **Credenciais Padrão:**
        * **User:** `Admin`
        * **Password:** `Admin123@`
    * Após o login com sucesso, a sessão `IsAdmin` é ativada e o menu superior exibe as opções de gestão.
<img width="1913" height="1079" alt="image" src="https://github.com/user-attachments/assets/9aa05d13-2712-4288-82f7-05c740afd031" />


**Opções de Gestão:**
<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/5cbeccf4-933a-45ab-966b-b7cb357c2fcc" />




* **Criar Novo Conteúdo:**
    * Clique em **"Novo Conteúdo"** (rota `/academy/create`).
    * **Campos Obrigatórios:** Título, Resumo, Conteúdo (Max 2000 caracteres) e Categoria.
    * **Conteúdo Rico:** O campo "Corpo do Artigo" aceita texto longo para posts de blog.
    * **Multimédia:** Insira URLs válidas para a Imagem de Capa e Vídeo do YouTube.
    * Ao salvar, o sistema valida os dados; se houver erro (ex: resumo muito longo), o formulário é recarregado com mensagens de alerta.

<img width="1588" height="1079" alt="image" src="https://github.com/user-attachments/assets/ff4bf7df-315b-4824-ab56-55c9d8e88d79" />



* **Edição:**
<img width="1592" height="1079" alt="image" src="https://github.com/user-attachments/assets/84061892-b98d-4200-887c-d39f0605900a" />



* **Exclusão:**
<img width="1915" height="1079" alt="image" src="https://github.com/user-attachments/assets/5fdd946d-b89b-4b02-8a89-772bb7ab24da" />



---

## Como Rodar o Projeto

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) instalado.
* Acesso a um banco de dados **Oracle**.
* Visual Studio 2022 ou VS Code.


#### 1. Entrando na pasta do projeto
Antes de executar os comandos, certifique-se de entrar na pasta da aplicação:
```
cd SyncMe
```
#### 2. Configuração do Banco de Dados (Connection String)
O projeto espera uma conexão com o Oracle. Você deve configurar a string de conexão.
Edite o arquivo appsettings.json na raiz do projeto e substitua os valores:
```bash
"ConnectionStrings": {
  "OracleConnection": "Data Source=seu_datasource_oracle;User Id=seu_usuario;Password=sua_senha;"
}
```

#### 3. Aplicando Migrations
Para criar as tabelas no banco de dados, execute o comando abaixo na raiz do projeto (onde está o arquivo .csproj):
```
dotnet ef database update
```

#### 4. Executando a Aplicação
Após configurar o banco, inicie o servidor apertando F5 ou abrindo o terminal e digitando (Lembre-se: Você deve estar dentro da pasta SyncMe):
```
dotnet run
```




