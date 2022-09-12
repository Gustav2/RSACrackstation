using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Backend;

namespace RSACrackstation.Controllers;

public class ApiController : Controller{
    public string[] GetFactors(string inputNum){
        var cracker = new RSACracker(inputNum);
        return cracker.GetFactors();
    }

    public Dictionary<string, string> Decipher(string p, string q, string e, string ct, bool isHex){
        var cracker = new RSACracker(p, q);
        cracker.E = BigInteger.Parse(e);

        var output = new Dictionary<string, string>();
        output["N"] = cracker.N.ToString();
        output["d"] = cracker.GetD().ToString();

        if (ct is null){
            output["pt"] = "";
            output["ptAscii"] = "";
        }
        else{
            var pt = cracker.GetPlainText(ct, isHex).ToString();
            output["pt"] = pt;
            try{
                output["ptAscii"] = cracker.ConvertToAscii(pt);
            }
            catch{
                output["ptAscii"] = "Could not convert to ASCII";
            }
        }
        return output;
    }

    public string Encrypt(string N, string e, string pt, int inputType = 0){
        var encryptor = new RSAEncrypter(N, e);
        encryptor.E = BigInteger.Parse(e);
        switch (inputType){
            case 0:
                // Ascii
                pt = encryptor.FromAscii(pt);
                break;
            case 1:
                // Decimal
                // Do nothing
                break;
            case 2:
                // Hex
                pt = encryptor.FromHex(pt);
                break;
        }

        Console.WriteLine(pt);
        return encryptor.Encrypt(pt);
    }
}