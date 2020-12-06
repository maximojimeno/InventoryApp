

$(document).ready(function () {

  

    //abrir y cerrar lupa
    $(document).on('click', '.search-wrapper', function () {

        if ($('.search-wrapper').attr('class') == 'search-wrapper') {
            $('.search-wrapper').addClass('active');
        } else {
            $('.search-wrapper').removeClass('active');
        }
      
    });

    //abrir logout
    $(document).on('click', '#logout', function () {
        if ($('#log').hasClass('show')) {
            $('#log').removeClass('show')
        } else {
            $('#log').addClass('show')

        }     

    });

    //abrir y cerrar menu pc
    $(document).on('click', '#hamburger', function () {
        console.log()
        if (!$('#app-container').hasClass('closed-sidebar-mobile')) {


            if ($('#hamburger').hasClass('is-active')) {
                $('#hamburger').removeClass('is-active');
                $('#app-container').removeClass('closed-sidebar');


            } else {
                $('#hamburger').addClass('is-active');
                $('#app-container').addClass('closed-sidebar');
            }

        } else {
            if ($('#hamburger').hasClass('is-active')) {
                $('.hamburger').removeClass('is-active');
                $('#app-container').removeClass('sidebar-mobile-open');

            } else {
                $('.hamburger').addClass('is-active');
                $('#app-container').addClass('sidebar-mobile-open');

            }

        }

    });


    //ojo pasar a un media query
    $(window).resize(function () {
        if ($(this).width() < 1250) {
            $(".app-container").addClass("closed-sidebar-mobile closed-sidebar");
        } else {
            $(".app-container").removeClass("closed-sidebar-mobile closed-sidebar");
        }
    });

    //menu despegable 
    $(document).on('click', '#menu-1', function () {

        if ($('#menu-1 .mm-collapse').hasClass('mm-show')) {
            $('#menu-1 .mm-collapse').removeClass('mm-show');
        } else {
            $('#menu-1 .mm-collapse').addClass('mm-show');
        }
    });

    $(document).on('click', '#menu-4', function () {

        if ($('#menu-4 .mm-collapse').hasClass('mm-show')) {
            $('#menu-4 .mm-collapse').removeClass('mm-show');
        } else {
            $('#menu-4 .mm-collapse').addClass('mm-show');
        }
    });


    //menu despegable 
    $(document).on('click', '#menu-2', function () {

        if ($('#menu-2 .mm-collapse').hasClass('mm-show')) {
            $('#menu-2 .mm-collapse').removeClass('mm-show');
        } else {
            $('#menu-2 .mm-collapse').addClass('mm-show');
        }
    });

    $(document).on('click', '.abrir-2', function () {

        setTimeout(function () { $('#menu-2 .mm-collapse').addClass('mm-show') }, 100);
          
        
    });

    $(document).on('click', '.abrir-4', function () {

        setTimeout(function () { $('#menu-4 .mm-collapse').addClass('mm-show') }, 100);


    });

    $(document).on('click', '.abrir-1', function () {

        setTimeout(function () { $('#menu-1 .mm-collapse').addClass('mm-show') }, 100);


    });

    //dejar selecionado cuando hace click n el menu
    $('body').on('click', '.metismenu a', function () {
        $('.mm-active .metismenu-icon').css('opacity', '.4');
        $('.metismenu a').removeClass('mm-active');
        $(this).addClass('mm-active');
       
        $('.mm-active .metismenu-icon').css('opacity','1');
    })



});

    function customComfirm(titulo, mensaje, tipo) {
        return new Promise((resolve) => {
            Swal.fire({
                title: titulo,
                text: mensaje,
                icon: tipo,
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.value) {
                    resolve(true);
                } else {
                    resolve(false);
                }
            })
        });

    }

function avisoAlert(titulo,tipo,tiempo,position) {
    return new Promise((resolve) => {
        Swal.fire({
            position:"center",
            icon: "success",
            title: titulo,
            showConfirmButton: false,
            timer: tiempo
        })
    resolve(true);
    });
}
