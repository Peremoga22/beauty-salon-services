window.loadScripts = () => {
    // initialization of HSMegaMenu component
    $('.js-mega-menu').HSMegaMenu({
        event: 'hover',
        pageContainer: $('.container'),
        breakpoint: 767.98,
        hideTimeOut: 0
    });

    // initialization of svg injector module
    $.HSCore.components.HSSVGIngector.init('.js-svg-injector');

    // initialization of header
    $.HSCore.components.HSHeader.init($('#header'));

    // initialization of unfold component
    //$.HSCore.components.HSUnfold.init($('[data-unfold-target]'), {
    //    afterOpen: function () {
    //        $(this).find('input[type="search"]').focus();
    //    }
    //});

    // initialization of malihu scrollbar
    $.HSCore.components.HSMalihuScrollBar.init($('.js-scrollbar'));

    // initialization of show animations
    $.HSCore.components.HSShowAnimation.init('.js-animation-link');

    // initialization of form validation
    $.HSCore.components.HSValidation.init('.js-validate', {
        rules: {
            confirmPassword: {
                equalTo: '#signupPassword'
            }
        }
    });

    // initialization of forms
    $.HSCore.components.HSFocusState.init();

    // initialization of go to
    $.HSCore.components.HSGoTo.init('.js-go-to');


    

    
    // initialization of popups
    $.HSCore.components.HSFancyBox.init('.js-fancybox');
   
    
    // initialization of text editors
    //$.HSCore.components.HSSummernoteEditor.init('.js-summernote-editor');

   

    // initialization of forms
    $.HSCore.components.HSFocusState.init();
  

    // initialization of datatables
    //$.HSCore.components.HSDatatables.init('.js-datatable');
    //var calendarEl = document.getElementById('calendar');

    //var calendar = new FullCalendar.Calendar(calendarEl, {
    //    plugins: ['dayGrid']
    //});

    // initialization of range datepicker
    $.HSCore.components.HSRangeDatepicker.init('.js-range-datepicker');

    // initialization of forms
    $.HSCore.components.HSFocusState.init();

    // initialization of autonomous popups
    $.HSCore.components.HSModalWindow.init('.js-modal-window', {
        autonomous: true
    });

    // initialization of form validation
    $.HSCore.components.HSValidation.init('.js-validate');

    // initialization of unfold component
    $.HSCore.components.HSUnfold.init($('[data-unfold-target]'));
  

}
function initMap() {
    var coordinates = { lat: 47.212325, lng: 38.933663 },

        map = new google.maps.Map(document.getElementById('map'), {
            center: coordinates
        });
}
function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}
window.setTitle =
    (text) => {
        document.title = text;
    }

//document.addEventListener('DOMContentLoaded', function () {
//    var calendarEl = document.getElementById('calendar');

//    var calendar = new FullCalendar.Calendar(calendarEl, {
//        plugins: ['dayGrid']
//    });
//    var calendar = new FullCalendar.Calendar(calendarEl, {
//        plugins: ['dayGrid', 'timeGrid', 'list'] // an array of strings!
//    });
//    calendar.render();
//});