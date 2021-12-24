var formContext;
function onLoad(executionContext){
    //debugger;
    
    formContext = executionContext.getFormContext();
    checkEndDate();
    if(formContext.ui.getFormType()!=1){
        formContext.getControl("new_newtopic").setDisabled(true); 
    }
    
}
function checkEndDate(){
    debugger;
    if(formContext.getAttribute("new_endsla").getValue() != null ){
        var endDate = formContext.getAttribute("new_endsla").getValue().getTime();
        var now = new Date().getTime();
        var timeleft = endDate - now;
            
        var days = Math.floor(timeleft / (1000 * 60 * 60 * 24));
        var hours = Math.floor((timeleft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    
    if (endDate < Date.now()) {
       formContext.ui.setFormNotification
        (formContext.getAttribute("new_endsla").getValue() + " Service time is over!", 'ERROR', 'serviceWarning');
    }
    else{
        formContext.ui.setFormNotification(
            (days)+ " days " + (hours) + " hours "  + " left until SLA is over", 'WARNING', 'serviceCaution');
    }
}
}
