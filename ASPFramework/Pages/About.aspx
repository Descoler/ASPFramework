<%@ Page Title="Acerca de" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ASPFramework.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>...</h2>
        <p>Esta página  web ha sido realizada con la tecnología Web Forms utilizando ASP .NET y HTML para el front end y C# .NET para el back end, utilizando una plantilla 
            de Web Forms de Microsoft Visual Studio Community 2022.</p> 
            <p style="text-decoration: underline; font-weight: bold">    Funcionalidad    </p> 
            <p>Al cargar la pagina el metodo PageLoad carga los productos obtenidos del API. REST proporcionado http://82.98.132.218:6587/api/productos. Los productos se presentan en una tabla HTML de cuatro campos: Foto, codigo de producto, titulo, precio.</p>
            <p>Las fotos las proporciona el API. REST http://82.98.132.218:6587/images/CODIGO_DE_PRODUCTO.jpg. La función, de la clase ValuesCotroller.cs, getFoto(string id) 
                hará la consulta a la url indicada substituyendo la parte "CODIGO DE PRDUCTO" por el código de producto recibido por parámetro. La respuesta de la consulta la convienrte en un archivo
                de imágen. Las imágenes que existen, porque hay otros productos que no tienen imagen, serán almacendas dentro de la carpeta Resources\Images que pertenece al proyecto, 
                desde donde son recuperadas para poder ser mostradas en la página. Los productos que no tienen imagen mostrarán la imagen por defecto SinFoto.jpg.
            </p>
            <p>
               La página  nos va a permitir, movernos entre las paginaciones que se crean al consultar la url http://82.98.132.218:6587/api/productos?offset=0&limit=20 añadiendo
                los parámetros offset, que va a indicar la paginación que se cargará del total de productos solicitados, y limit que se encarga de decidir cuantos productos
                seran cargados en esa paginación.
            </p>
            <p>
                Los botones Prev y Next de la página nos permiten mostrar las distintas paginaciones de los productos resultantes de la consulta realizada al API REST, en caso de
                que el resultado devuelva más de 20 productos.
            </p>
            <p> 
                Se ofrecen dos opciones de búsqueda: 
            </p>
            <p>  
                - "Buscar productos" realizará una consulta a la url http://82.98.132.218:6587/api/productos?offset=0&limit=20 añadiendo el 
                parámetro busca="contenido de cuadro de texto". La función GetProductosPaginados(string parametros) será la que se encargue de añadir los parámetros recibidos 
                por la función a la url de consulta y devolver los productos resultantes de la consulta.
            </p>
            <p>
                - "Buscar codigo de barras" lanzará la consulta a la url http://82.98.132.218:6587/api/productos?offset=0&limit=20 añadiendo el parámetro
                codigobarras="codigo de producto". La función GetProductosPaginados(string parametros) será la que se encargue de añadir los parámetros recibidos 
                por la función a la url de consulta y devolver los productos resultantes de la consulta.
            </p>
            <p>
                Todos los resultados recibidos por las consultas, tanto las que se reciben tras realizar una búsqueda como las que se cargan sin un parámetro de búsqueda,
                se pueden ordenar con las opciones "Titulo Ascendente", "Titulo Descendente", "Codigo Ascendente" y "Codigo Descendente". Cada opcion proporciona un 
                valor (tituloasc,titulodesc, codigoasc y codigodesc respectivamente) para el parámetro orden=valor que se añadirá a la url con los parámetros que se esté 
                mostrando en ese momento.  La función que se encarga de realizar las consultas paginadas y ordenadas es GetProductosPaginados(string url2,string parametros)
                que recibe como parámetros la url que se este mostrando en ese momento y los parámetros asociados al orden en que se mostrarán.
            </p> 
    </main>
</asp:Content>
