$(document).ready(function (){
    
    var quantity1 = Number($("#shirt-quantity-btn-1").val());
    $("#increase-btn-1").click(function(){
        quantity1 += 1;
        $("#shirt-quantity-btn-1").text(quantity1);
        
    });	
    $("#decrease-btn-1").click(function(){
        if(quantity1 > 0){
            quantity1 -= 1;
            $("#shirt-quantity-btn-1").text(quantity1);
        }
        else{
            quantity1 = 0;
        }
        
    });	

    var quantity2 = Number($("#shirt-quantity-btn-2").val());
    $("#increase-btn-2").click(function(){
        quantity2 += 1;
        $("#shirt-quantity-btn-2").text(quantity2);
        
    });	
    $("#decrease-btn-2").click(function(){
        if(quantity2 > 0){
            quantity2 -= 1;
            $("#shirt-quantity-btn-2").text(quantity2);
        }
        else{
            quantity2 = 0;
        }
        
    });

});