# PAC 4 – Calculadora WPF

## Descripció
Aplicació de Windows Presentation Foundation (WPF) que implementa una calculadora amb:
- Operacions bàsiques: suma (+), resta (−), multiplicació (×), divisió (÷)
- Suport per a expressions encadenades amb precedència d’operadors
- Gestió de decimals segons cultura local
- Botons d'eliminació (⌫) i neteja (C)
- Validació d’errors (p. ex. divisió per zero, sintaxi invàlida)
- Estils integrats per dígits, operadors i accions de perill

## Autor
Biel Reniu Valdés

## Índex
1. [Requisits](#requisits)
2. [Instal·lació](#instal·lació)
3. [Ús](#ús)
4. [Exemples](#exemples)
5. [Estils](#estils)
6. [Captures de pantalla](#captures-de-pantalla)
7. [Conclusió](#conclusió)
8. [Llicència](#llicència)

## Requisits
- .NET 6.0 SDK o superior
- Windows 10/11
- Visual Studio 2022 (recomanat)

## Instal·lació
```bash
git clone <URL-del-repositori>
cd PAC_4___Calculadora
```  
Obre la solució `PAC_4___Calculadora.sln` a Visual Studio i prem **Build Solution**.

## Ús
1. Executa l’aplicació (F5 a Visual Studio o doble clic a l’executable).
2. Introdueix nombres, operadors i punt decimal.
3. Prem `=` per avaluar.
4. Prem `C` per reiniciar l’expressió o `⌫` per eliminar l’últim caràcter.

## Exemples
- `9 × 9 − 3 = 78`
- `5 + 3 * 2 = 11` (respecta precedència)
- `5 / 2 = 2,5`, després `× 4 = 10`
- `8 ÷ 0` mostra `Error: Divisió per zero`
- Sintaxi invàlida (`+5`, `5*−`) mostra `Error`

## Estils
Els estils estan definits al fitxer XAML sota `<Window.Resources>`:
```xml
<Style x:Key="DigitButton" TargetType="Button">
  <Setter Property="Background" Value="#F0F0F0"/>
  <Setter Property="Foreground" Value="Black"/>
</Style>
<Style x:Key="OperatorButton" TargetType="Button">
  <Setter Property="Background" Value="#FFB75E"/>
  <Setter Property="Foreground" Value="White"/>
</Style>
<Style x:Key="WarningButton" TargetType="Button">
  <Setter Property="Background" Value="#E74C3C"/>
  <Setter Property="Foreground" Value="White"/>
</Style>
```

## Captures de pantalla

### Interfície amb estils aplicats
![Interfície inicial](screenshots/initial.png)

### Error per divisió per zero
![Error divisió per zero](screenshots/divzero.png)

### Error de sintaxi invàlida
![Error sintaxi invàlida](screenshots/syntax-error.png)

## Conclusió
Aquest projecte demostra:
- Separació clara entre XAML (vista) i code-behind (lògica).
- Ús de `DataTable.Compute` per processar expressions amb precedència.
- Maneig de culturals per decimals i detecció d’errors com divisió per zero.
- Integració d'estils al mateix XAML per un sol fitxer de disseny.

## Llicència
MIT © Biel Reniu Valdés
