// JavaScript functions for Blazor interop
// ouvre une fenêtre d'alerte avec la valeur 'n' et un message d'avertissement
window.displayAlert = (n) => {
    alert("Attention, vous êtes à la valeur " + n + ". Quand vous arriverez à la limite, l'incrémentation sera bloquée.");
};

// ouvre une fenêtre de prompt pour demander à l'utilisateur d'entrer une valeur initiale, puis retourne cette valeur
window.askInitial = () => {
    let number = prompt("Veuillez entrer une valeur initiale :");
    return number;
};


// - mise en place d'un systeme pour faire un appel d'une methode c# par le javascript
// - 2 methode possible utilsiation d'une variable globale counterComponent
// - ou création d'une classe avec la methode invoke qui est stockée dans une variable window.incrementBy3Instance

class Increment3 {
    constructor(dotNetObject) {
        this.dotNetObject = dotNetObject;
    }
    // - method qui appelle la methode C#
    IncrementBy3() {
        this.dotNetObject.invokeMethodAsync("JSIncrementBy3");
    }
}

// var counterComponent;

// - création de l'instance de reférence pour faire appel à une method C# depuis JavaScript
// - cette methode est appelée depuis le code C# avec pour parametre un object Dotnet 
window.storeCounterReference = (dotNetObject) => {
    if (dotNetObject) {
        //counterComponent = dotNetObject;
        window.incrementBy3Instance = new Increment3(dotNetObject);
    }
          
};

// - fonction javascript appeler depuis la vue sur le onclick html
function IncrementBy3() {
    //if(counterComponent) {
    if (window.incrementBy3Instance) {
        //counterComponent.invokeMethodAsync("JSIncrementBy3");
        window.incrementBy3Instance.IncrementBy3();
        
    }
}