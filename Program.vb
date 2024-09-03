Imports System

Imports System.IO

Module Estacionamiento

    ' Funci�n para calcular el monto total a pagar
    Function CalcularMonto(horas As Integer) As Decimal
        If horas <= 1 Then
            Return 15
        Else
            Return 15 + (horas - 1) * 20
        End If
    End Function

    ' Funci�n para validar entrada de hora
    Function ValidarHora(mensaje As String) As DateTime
        Dim horaValida As DateTime
        While True
            Console.Write(mensaje)
            Dim entrada As String = Console.ReadLine()
            If DateTime.TryParse(entrada, horaValida) Then
                Return horaValida
            Else
                Console.WriteLine("Hora no v�lida. Por favor, intente nuevamente.")
            End If
        End While
    End Function

    ' Funci�n para validar que el NIT solo contenga n�meros
    Function ValidarNIT(mensaje As String) As String
        While True
            Console.Write(mensaje)
            Dim nit As String = Console.ReadLine()
            If IsNumeric(nit) Then
                Return nit
            Else
                Console.WriteLine("NIT no v�lido. Debe contener solo n�meros. Por favor, intente nuevamente.")
            End If
        End While
    End Function

    ' Funci�n para escribir en el archivo facturas.txt
    Sub EscribirFactura(nombre As String, nit As String, placa As String, horas As Integer, monto As Decimal)
        Dim rutaArchivo As String = "facturas.txt"
        Try
            Using escritor As StreamWriter = New StreamWriter(rutaArchivo, True)
                escritor.WriteLine("Nombre del Cliente: " & nombre)
                escritor.WriteLine("NIT: " & nit)
                escritor.WriteLine("Identificaci�n del Veh�culo: " & placa)
                escritor.WriteLine("Tiempo en el parqueo: " & horas & " horas")
                escritor.WriteLine("Monto Total a Pagar: Q" & monto.ToString("F2"))
                escritor.WriteLine("=========================================")
            End Using
            Console.WriteLine("Factura generada y guardada exitosamente en " & rutaArchivo)
        Catch ex As Exception
            Console.WriteLine("Error al escribir en el archivo: " & ex.Message)
        End Try
    End Sub

    ' Funci�n principal
    Sub Main()
        ' Ingreso de datos del cliente
        Console.Write("Ingrese el nombre del cliente: ")
        Dim nombre As String = Console.ReadLine()

        ' Validar que el NIT solo contenga n�meros
        Dim nit As String = ValidarNIT("Ingrese el NIT: ")

        Console.Write("Ingrese la identificaci�n del veh�culo (n�mero de placa): ")
        Dim placa As String = Console.ReadLine()

        ' Validaci�n de hora de entrada y salida
        Dim horaEntrada As DateTime = ValidarHora("Ingrese la hora de entrada (HH:mm): ")
        Dim horaSalida As DateTime = ValidarHora("Ingrese la hora de salida (HH:mm): ")

        ' Calcular tiempo en horas
        Dim horasEstacionamiento As Integer = CInt(Math.Ceiling((horaSalida - horaEntrada).TotalHours))

        ' Calcular monto a pagar
        Dim montoTotal As Decimal = CalcularMonto(horasEstacionamiento)

        ' Mostrar resumen de la transacci�n
        Console.WriteLine()
        Console.WriteLine("===== Resumen de la Transacci�n =====")
        Console.WriteLine("Nombre del Cliente: " & nombre)
        Console.WriteLine("Identificaci�n del Veh�culo: " & placa)
        Console.WriteLine("Tiempo en el parqueo: " & horasEstacionamiento & " horas")
        Console.WriteLine("Monto Total a Pagar: Q" & montoTotal.ToString("F2"))

        ' Escribir la factura en el archivo de texto
        EscribirFactura(nombre, nit, placa, horasEstacionamiento, montoTotal)
    End Sub

End Module

