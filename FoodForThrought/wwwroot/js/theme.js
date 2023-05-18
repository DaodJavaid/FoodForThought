

(function ($) {

    'use strict';

    function initMetisMenu() {
        //metis menu
        $("#side-menu").metisMenu();
    }

    function initLeftMenuCollapse() {
        // Left menu collapse
        $('#vertical-menu-btn').on('click', function () {
            $('body').toggleClass('enable-vertical-menu');
        });

        $('.menu-overlay').on('click', function () {
            $('body').removeClass('enable-vertical-menu');
            return;
        });
    }

   

   

    function init() {
        initMetisMenu();
        initLeftMenuCollapse();
        initActiveMenu();
        initComponents();
       Waves.init();
    }

    init();

})(jQuery)