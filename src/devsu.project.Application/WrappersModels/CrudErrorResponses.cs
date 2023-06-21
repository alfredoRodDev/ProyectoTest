using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.WrappersModels
{
    public static class CrudErrorResponses
    {
        public static string ResourceNotFound = "¡Este recurso no existe en la base de datos!";
        public static string ErrorAdding = "¡Error Agregando registro, por favor intente nuevamente!";
        public static string ErrorUpdating = "¡Error Actualizando registro, por favor intente nuevamente!";
        public static string ErrorDeleting = "¡Error eliminando registro, por favor intente nuevamente!";
        public static string ErrorCantDelete = "¡Este registro no puede ser eliminado porque está siendo usado por un recurso del sistema!";
        public static string ErrorRepeatedResource = "¡Ya existe un registro con iguales caracteristicas en la base de datos!";

    }
}
