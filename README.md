# Minuta de Relevamiento del Sistema de Gesti�n de Transporte de Granos : �AgriCargo�

- **GitHub**: [Repositorio AgriCargo API](https://github.com/MarucoMass/Agricargo-API)
- **Participantes**: Massonnat Mario y Francisco Palena

## 1. Contexto del Proyecto

AgriCargo es una aplicaci�n especializada en la gesti�n del transporte de granos exclusivamente por v�a mar�tima. Su objetivo es conectar a los productores y exportadores de granos con empresas de transporte mar�timo que ofrecen sus servicios, facilitando el proceso de b�squeda, reserva y gesti�n de viajes a trav�s de una interfaz intuitiva y robusta.

El sistema est� dise�ado para gestionar el transporte de granos, facilitando la interacci�n entre los clientes, las empresas de transporte y los administradores. Las principales entidades que participan en el sistema son los Usuarios (Clientes, Empresas, SuperAdministradores), los Viajes y los Transportes (Barcos), Favoritos y Reservas.

La aplicaci�n utiliza un sistema de autenticaci�n para asegurar que solo usuarios registrados puedan realizar reservas o gestionar barcos.

### Funcionalidades Principales

- **B�squeda de Viajes**: Los usuarios pueden buscar viajes disponibles en funci�n de sus necesidades sin necesidad de tener previamente una cuenta. La b�squeda puede realizarse por origen, destino y cantidad a transportar en toneladas.

- **Reserva de Viajes**: Los clientes registrados pueden realizar la reserva de un viaje, visualizar el precio por tonelada de grano y calcular el costo total seg�n la carga que deseen enviar.

- **Favoritos**: La plataforma permite que los clientes guarden ciertos viajes como "favoritos" para consultarlos m�s tarde.

- **Perfil de Usuario y Gesti�n de Reservas**: Cada cliente puede visualizar y gestionar sus reservas, cancelar reservas antes de la partida y revisar detalles del viaje.

- **Empresas de Transporte Mar�timo (Companies)**: Las empresas pueden registrarse, cargar sus barcos y gestionar sus viajes en la aplicaci�n.

- **Gesti�n de Barcos y Viajes**: Las empresas pueden registrar y gestionar barcos con informaci�n relevante, como capacidad y disponibilidad para transportar granos.

- **Sistema de Administraci�n Avanzado (SuperAdmin)**: El SuperAdmin tiene control sobre todos los usuarios, barcos y viajes, supervisando la plataforma y resolviendo disputas o problemas t�cnicos.

## 2. Entidades Principales

### 2.1 User (abstracto)
Atributos generales para todos los usuarios:
- **Atributos**: Id, Name, Email, Phone, TypeUser, Password
- **Tipos de usuarios**: Client, Company (admin), SuperAdmin

### 2.2 Client (Usuario Cliente)
Representa al cliente que realiza reservas de transporte.
- **Relaciones**:
  - Lista de reservas: `List<Reservation>`
  - Lista de favoritos: `List<Favorite>`

### 2.3 Company (Usuario Empresa/Admin)
Representa a la empresa que ofrece servicios de transporte mar�timo.
- **Atributos**:
  - CompanyName
  - Lista de barcos: `List<Ship>`

### 2.4 SuperAdmin
Usuario con permisos avanzados para gestionar toda la plataforma.

## 3. Entidades Operacionales

### 3.1 Favorite (Favoritos)
Permite a los clientes marcar viajes como favoritos.
- **Atributos**: Id, TripId, ClientId
- **Relaci�n**: Relacionado con un cliente y un viaje espec�fico

### 3.2 Reservation (Reservaci�n)
Maneja las reservas realizadas por los clientes.
- **Atributos**: Id, ClientId, Client, TripId, Trip, PurchasePrice, PurchaseAmount, ReservationStatus, purchaseDate, DepartureDate, ArriveDate
- **Relaci�n**: Relacionada con un cliente y un viaje

### 3.3 Trip (Viaje)
Representa los viajes que ofrecen las empresas.
- **Atributos**: Id, Origin, Destination, Price, DepartureDate, ArriveDate, TripState, AvailableCapacity, ShipId, Ship
- **Relaci�n**: Relacionado con un barco espec�fico y puede tener m�ltiples reservas

### 3.4 Ship (Barco)
Entidad que representa los barcos disponibles para el transporte.
- **Atributos**: Id, TypeShip, Capacity, Captain, ShipPlate, AvailabilityStatus, Trips, CompanyId, Company
- **Relaci�n**: Cada barco puede tener m�ltiples viajes asociados y est� relacionado con una empresa

## 4. Relaciones Importantes

- Un **Client** puede tener m�ltiples **Reservations** y **Favorites**.
- Una **Company** puede tener m�ltiples **Ships**.
- Un **Ship** puede tener m�ltiples **Trips**.
- Un **Trip** puede ser favorito de m�ltiples **Clients** y tener m�ltiples **Reservations**.
