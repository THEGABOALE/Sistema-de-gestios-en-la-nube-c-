using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static List<Usuario> usuarios = new List<Usuario>();
    static List<Cliente> clientes = new List<Cliente>();
    static List<Trabajador> trabajadores = new List<Trabajador>();
    static List<Recurso> recursos = new List<Recurso>();
    static Usuario usuarioActual = null;

    static void Main()
    {
        CargarUsuarios();
        while (true)
        {
            if (usuarioActual == null)
            {
                MostrarMenuInicial();
            }
            else
            {
                MostrarMenuGestion();
            }
        }
    }

    // --- Menús ---

    static void MostrarMenuInicial()
    {
        Console.WriteLine("\n=== Menú Inicial ===");
        Console.WriteLine("1. Iniciar sesión");
        Console.WriteLine("2. Registrarse");
        Console.WriteLine("0. Salir");

        Console.Write("Seleccione una opción: ");
        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                IniciarSesion();
                break;
            case "2":
                Registrarse();
                break;
            case "0":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }
    }

    static void MostrarMenuGestion()
    {
        Console.WriteLine("\n=== Menú de Gestión ===");
        Console.WriteLine("1. Agregar Cliente");
        Console.WriteLine("2. Mostrar Clientes");
        Console.WriteLine("3. Editar Cliente");
        Console.WriteLine("4. Eliminar Cliente");
        Console.WriteLine("5. Agregar Trabajador");
        Console.WriteLine("6. Mostrar Trabajadores");
        Console.WriteLine("7. Editar Trabajador");
        Console.WriteLine("8. Eliminar Trabajador");
        Console.WriteLine("9. Asignar Recurso");
        Console.WriteLine("10. Mostrar Recursos");
        Console.WriteLine("0. Cerrar sesión");

        Console.Write("Seleccione una opción: ");
        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                AgregarCliente();
                break;
            case "2":
                MostrarClientes();
                break;
            case "3":
                SeleccionarClienteParaEditar();
                break;
            case "4":
                SeleccionarClienteParaEliminar();
                break;
            case "5":
                AgregarTrabajador();
                break;
            case "6":
                MostrarTrabajadores();
                break;
            case "7":
                SeleccionarTrabajadorParaEditar();
                break;
            case "8":
                SeleccionarTrabajadorParaEliminar();
                break;
            case "9":
                AsignarRecurso();
                break;
            case "10":
                MostrarRecursos();
                break;
            case "0":
                usuarioActual = null;
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }
    }

    // --- Gestión de Clientes ---

    static void AgregarCliente()
    {
        Console.WriteLine("\n=== Agregar Cliente ===");
        Console.Write("Ingrese el nombre del cliente: ");
        string nombre = Console.ReadLine();
        if (!ValidarNombreCliente(nombre))
        {
            Console.WriteLine("Nombre inválido.");
            return;
        }

        Console.Write("Ingrese el correo del cliente: ");
        string correo = Console.ReadLine();
        if (!ValidarCorreo(correo))
        {
            Console.WriteLine("Correo inválido.");
            return;
        }

        clientes.Add(new Cliente { Nombre = nombre, Correo = correo });
        Console.WriteLine("Cliente agregado exitosamente.");
    }

    static void MostrarClientes()
    {
        Console.WriteLine("\n=== Lista de Clientes ===");
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"Nombre: {cliente.Nombre}, Correo: {cliente.Correo}");
        }
    }

    static void SeleccionarClienteParaEditar()
    {
        if (clientes.Count == 0)
        {
            Console.WriteLine("No hay clientes registrados para editar.");
            return;
        }

        Console.WriteLine("Clientes registrados:");
        for (int i = 0; i < clientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {clientes[i].Nombre}");
        }

        Console.Write("Seleccione el cliente por número: ");
        if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= clientes.Count)
        {
            EditarCliente(clientes[seleccion - 1]);
        }
        else
        {
            Console.WriteLine("Selección inválida.");
        }
    }

    static void EditarCliente(Cliente cliente)
    {
        Console.Write("Ingrese el nuevo nombre del cliente (dejar en blanco para mantener el actual): ");
        string nuevoNombre = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevoNombre) && !ValidarNombreCliente(nuevoNombre))
        {
            Console.WriteLine("El nombre es inválido.");
            return;
        }
        if (!string.IsNullOrEmpty(nuevoNombre))
        {
            cliente.Nombre = nuevoNombre;
        }

        Console.Write("Ingrese el nuevo correo del cliente (dejar en blanco para mantener el actual): ");
        string nuevoCorreo = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevoCorreo) && !ValidarCorreo(nuevoCorreo))
        {
            Console.WriteLine("El correo es inválido.");
            return;
        }
        if (!string.IsNullOrEmpty(nuevoCorreo))
        {
            cliente.Correo = nuevoCorreo;
        }

        Console.WriteLine("Cliente editado exitosamente.");
    }

    static void SeleccionarClienteParaEliminar()
    {
        if (clientes.Count == 0)
        {
            Console.WriteLine("No hay clientes registrados para eliminar.");
            return;
        }

        Console.WriteLine("Clientes registrados:");
        for (int i = 0; i < clientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {clientes[i].Nombre}");
        }

        Console.Write("Seleccione el cliente por número: ");
        if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= clientes.Count)
        {
            clientes.RemoveAt(seleccion - 1);
            Console.WriteLine("Cliente eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Selección inválida.");
        }
    }

    // --- Gestión de Trabajadores ---

    static void AgregarTrabajador()
    {
        Console.WriteLine("\n=== Agregar Trabajador ===");
        Console.Write("Ingrese el nombre del trabajador: ");
        string nombre = Console.ReadLine();
        if (!ValidarNombreTrabajador(nombre))
        {
            Console.WriteLine("Nombre inválido.");
            return;
        }

        trabajadores.Add(new Trabajador { Nombre = nombre });
        Console.WriteLine("Trabajador agregado exitosamente.");
    }

    static void MostrarTrabajadores()
    {
        Console.WriteLine("\n=== Lista de Trabajadores ===");
        foreach (var trabajador in trabajadores)
        {
            Console.WriteLine($"Nombre: {trabajador.Nombre}");
        }
    }

    static void SeleccionarTrabajadorParaEditar()
    {
        if (trabajadores.Count == 0)
        {
            Console.WriteLine("No hay trabajadores registrados para editar.");
            return;
        }

        Console.WriteLine("Trabajadores registrados:");
        for (int i = 0; i < trabajadores.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {trabajadores[i].Nombre}");
        }

        Console.Write("Seleccione el trabajador por número: ");
        if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= trabajadores.Count)
        {
            EditarTrabajador(trabajadores[seleccion - 1]);
        }
        else
        {
            Console.WriteLine("Selección inválida.");
        }
    }

    static void EditarTrabajador(Trabajador trabajador)
    {
        Console.Write("Ingrese el nuevo nombre del trabajador (dejar en blanco para mantener el actual): ");
        string nuevoNombre = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevoNombre) && !ValidarNombreTrabajador(nuevoNombre))
        {
            Console.WriteLine("El nombre es inválido.");
            return;
        }
        if (!string.IsNullOrEmpty(nuevoNombre))
        {
            trabajador.Nombre = nuevoNombre;
        }

        Console.WriteLine("Trabajador editado exitosamente.");
    }

    static void SeleccionarTrabajadorParaEliminar()
    {
        if (trabajadores.Count == 0)
        {
            Console.WriteLine("No hay trabajadores registrados para eliminar.");
            return;
        }

        Console.WriteLine("Trabajadores registrados:");
        for (int i = 0; i < trabajadores.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {trabajadores[i].Nombre}");
        }

        Console.Write("Seleccione el trabajador por número: ");
        if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= trabajadores.Count)
        {
            trabajadores.RemoveAt(seleccion - 1);
            Console.WriteLine("Trabajador eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Selección inválida.");
        }
    }

    // --- Gestión de Recursos ---

    static void AsignarRecurso()
    {
        Console.WriteLine("\n=== Asignar Recurso ===");
        Console.Write("Ingrese el nombre del recurso: ");
        string nombreRecurso = Console.ReadLine();
        if (!ValidarNombreRecurso(nombreRecurso))
        {
            Console.WriteLine("Nombre del recurso inválido.");
            return;
        }

        Console.WriteLine("¿Asignar a 1) Cliente o 2) Trabajador?");
        string tipo = Console.ReadLine();
        if (tipo == "1")
        {
            MostrarClientes();
            Console.Write("Seleccione el número del cliente: ");
            if (int.TryParse(Console.ReadLine(), out int seleccionCliente) && seleccionCliente > 0 && seleccionCliente <= clientes.Count)
            {
                recursos.Add(new Recurso { Nombre = nombreRecurso, AsignadoA = clientes[seleccionCliente - 1].Nombre, TipoAsignacion = "Cliente" });
                Console.WriteLine("Recurso asignado al cliente exitosamente.");
            }
            else
            {
                Console.WriteLine("Selección inválida.");
            }
        }
        else if (tipo == "2")
        {
            MostrarTrabajadores();
            Console.Write("Seleccione el número del trabajador: ");
            if (int.TryParse(Console.ReadLine(), out int seleccionTrabajador) && seleccionTrabajador > 0 && seleccionTrabajador <= trabajadores.Count)
            {
                recursos.Add(new Recurso { Nombre = nombreRecurso, AsignadoA = trabajadores[seleccionTrabajador - 1].Nombre, TipoAsignacion = "Trabajador" });
                Console.WriteLine("Recurso asignado al trabajador exitosamente.");
            }
            else
            {
                Console.WriteLine("Selección inválida.");
            }
        }
        else
        {
            Console.WriteLine("Selección inválida.");
        }
    }

    static void MostrarRecursos()
    {
        Console.WriteLine("\n=== Lista de Recursos ===");
        foreach (var recurso in recursos)
        {
            Console.WriteLine($"Recurso: {recurso.Nombre}, Asignado a: {recurso.AsignadoA} ({recurso.TipoAsignacion})");
        }
    }

    // --- Validaciones ---

    static bool ValidarNombreCliente(string nombre)
    {
        return Regex.IsMatch(nombre, @"^[a-zA-Z\s]+$");
    }

    static bool ValidarNombreTrabajador(string nombre)
    {
        return Regex.IsMatch(nombre, @"^[a-zA-Z\s]+$");
    }

    static bool ValidarCorreo(string correo)
    {
        return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    static bool ValidarNombreRecurso(string nombre)
    {
        return Regex.IsMatch(nombre, @"^[a-zA-Z0-9\s]+$");
    }

    // --- Autenticación ---

    static void IniciarSesion()
    {
        Console.WriteLine("\n=== Iniciar Sesión ===");
        Console.Write("Ingrese su nombre de usuario: ");
        string nombreUsuario = Console.ReadLine();

        Console.Write("Ingrese una contraseña: ");
        string contrasena = Console.ReadLine();

        Usuario usuario = usuarios.Find(u => u.NombreUsuario == nombreUsuario && u.Contrasena == contrasena);

        if (usuario != null)
        {
            usuarioActual = usuario;
            Console.WriteLine("Inicio de sesión exitoso.");
        }
        else
        {
            Console.WriteLine("Nombre de usuario o contraseña incorrectos.");
        }
    }

    static void Registrarse()
    {
        Console.WriteLine("\n=== Registrarse ===");
        Console.Write("Ingrese un nombre de usuario: ");
        string nombreUsuario = Console.ReadLine();

        Console.Write("Ingrese una contraseña: ");
        string contrasena = Console.ReadLine();

        usuarios.Add(new Usuario { NombreUsuario = nombreUsuario, Contrasena = contrasena });
        Console.WriteLine("Registro exitoso. Ahora puede iniciar sesión.");
    }

    // --- Clases ---

    class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
    }

    class Cliente
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
    }

    class Trabajador
    {
        public string Nombre { get; set; }
    }

    class Recurso
    {
        public string Nombre { get; set; }
        public string AsignadoA { get; set; }
        public string TipoAsignacion { get; set; } // Puede ser "Cliente" o "Trabajador"
    }

    static void CargarUsuarios()
    {
        usuarios.Add(new Usuario { NombreUsuario = "admin", Contrasena = "admin" });
    }
}
