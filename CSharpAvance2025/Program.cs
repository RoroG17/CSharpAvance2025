// See https://aka.ms/new-console-template for more information
Console.WriteLine("---------- CALCULATRICE ----------");

string calcul = Console.ReadLine();

if (string.IsNullOrEmpty(calcul))
{
    Console.WriteLine("Aucun calcul fourni");
    return;
}
var elements = calcul.Split(' ');

double a = Convert.ToDouble(elements[0]);
double b = Convert.ToDouble(elements[2]);
string operateur = elements[1];

double result = 0;
switch (operateur)
{
    case "+":
        result = a + b;
        break;

    case "-":
        result = a - b;
        break;

    case "*":
        result = a * b;
        break;

    case "/":
        if (b == 0)
        {
            Console.WriteLine("Division par zéro impossible");
        }
        else
        {
            result = a / b;
        }
        break;

    default:
        Console.WriteLine("Opérateur non reconnu");
        break;
}

Console.WriteLine(result);