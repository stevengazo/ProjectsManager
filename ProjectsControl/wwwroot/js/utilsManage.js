var Rolid = null;


function SetRolId(element) {
    var select = element;
    Rolid = select.options[select.selectedIndex].value   
    console.log(Rolid);
}


function SendRol() {
    debugger;
    var valueId = document.getElementById("idUser").innerText;
    if (Rolid !== null) {
        var userid = valueId;        
        window.location.href = `@Url.Action("SetRoleUser", "Manage", new { area = "Admin", id = "${userid}", rolid="${Rolid}"})`;
    }

}
