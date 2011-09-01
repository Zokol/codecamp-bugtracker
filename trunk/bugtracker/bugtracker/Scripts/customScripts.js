function showModalPopup(s) {

        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        //Set height and width to mask to fill up the whole screen
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

        //transition effect     
        $('#mask').fadeIn(1000);
        $('#mask').fadeTo("slow", 0.8);

        //Get the window height and width
        var winH = $(window).height();
        var winW = $(window).width();

        loadPartialView(s, 'dialog');

        //Set the popup window to center
        $('#dialog').css('top', '10px');
        $('#dialog').css('left', winW / 2 - $('#dialog').width() / 2);


        //transition effect
        $('#dialog').fadeIn(2000);

    };

function closeModalPopup() {
        $('#mask, .window').hide();
    };