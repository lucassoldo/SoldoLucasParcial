using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Parcial2.Models;

namespace Parcial2.Repository
{
    public class JsonRepository<T> : IRepository<T> where T : IEntidad
    {
        private readonly string _ruta;

        private static readonly JsonSerializerOptions _opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,   // guarda "Alineación" y no "Alineaci\u00F3n"
            Converters = { new JsonStringEnumConverter() }            // guarda "EnReparacion" y no 1
        };

        public JsonRepository(string ruta)
        {
            _ruta = ruta;
            string carpeta = Path.GetDirectoryName(_ruta);
            if (!string.IsNullOrEmpty(carpeta))
                Directory.CreateDirectory(carpeta);
        }

        // TODO 1 · Persistencia (1 de 2): LeerTodos funciona solo en el caso feliz.
        // >>> PROBLEMA: si el archivo no existe, está vacío o tiene JSON roto, explota con una excepción cruda.
        // Hacelo robusto:
        //   - archivo inexistente o vacío -> lista vacía
        //   - JSON dañado -> InvalidOperationException con un mensaje claro (por ejemplo "El archivo X está dañado.")
        public List<T> LeerTodos()
        {
            
            string json = File.ReadAllText(_ruta);
            if (json == null)
            {
                throw new InvalidOperationException($"el archivo{json} esta dañado");
            }
            return JsonSerializer.Deserialize<List<T>>(json, _opciones);
        }

        public void Agregar(T item)
        {
            var lista = LeerTodos();
            item.Id = lista.Count > 0 ? lista.Max(x => x.Id) + 1 : 1;
            lista.Add(item);
            Persistir(lista);
        }

        public T BuscarPorId(int id)
        {
            return LeerTodos().FirstOrDefault(x => x.Id == id);
        }

        // TODO 1 · Persistencia (2 de 2): reemplazar el elemento con el mismo Id y reescribir el archivo.
        // Si no existe, InvalidOperationException con un mensaje claro.
        public void Actualizar(T item)
        {
            var lista = LeerTodos();
            int indice = lista.FindIndex(x => x.Id == item.Id);
            if (indice == -1)
                throw new InvalidOperationException($"No existe el registro #{item.Id}.");
            lista[indice] = item;                           
            Persistir(lista);
        }

        private void Persistir(List<T> lista)
        {
            try
            {
                File.WriteAllText(_ruta, JsonSerializer.Serialize(lista, _opciones));
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("No se pudo guardar el archivo de datos: " + ex.Message);
            }
        }
    }
}
