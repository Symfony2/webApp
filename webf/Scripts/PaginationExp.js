

function initBoxes(increment, totalFrameAmount, div_iden, strings, prev, next) {
            
        $(div_iden).append(strings);
            
        if(totalFrameAmount<=increment)
                next.hide();
            else
                next.fadeIn(200);
        if(increment<=1)
                prev.hide();
            else
                prev.fadeIn(200);
    }

    function createPage(amountOfPages, currentFrame ) {
            
        var str = "";
        var lenghtOfFrame = 10;
        if (amountOfPages > currentFrame * lenghtOfFrame) {

            for (var i = (currentFrame - 1) * lenghtOfFrame; i < currentFrame * lenghtOfFrame; i++) {
                
                str += "<li><a id='" + i + "' onclick='clickMe(" + (i + 1) + ")'>" + (i + 1) + "</a></li>";
            }
        }
        else {
            for (var i = (currentFrame - 1) * lenghtOfFrame; i < amountOfPages; i++) {

                str += "<li><a id='" + i + "' onclick='clickMe(" + (i + 1) + ")'>" + (i + 1) + "</a></li>";
            }
        }

        return str;
    }

    