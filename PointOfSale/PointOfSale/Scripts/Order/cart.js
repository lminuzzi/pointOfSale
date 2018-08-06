$(function () {
    var $popover = $('.popover-markup>.trigger').popover({
        html: true,
        placement: 'bottom',
        content: function () {
            return $(this).parent().find('.content').html();
        }
    });

    // open popover & inital value in form
    var totalItens = [1];
    $('.popover-markup>.trigger').click(function (e) {
        e.stopPropagation();
        $(".popover-content input").each(function (i) {
            $(this).val(totalItens[i]);
        });
    });
    // close popover
    $(document).click(function (e) {
        if ($(e.target).is('.demise')) {
            $('.popover-markup>.trigger').popover('hide');
        }
    });
    // store form value when popover closed
    $popover.on('hide.bs.popover', function () {
        $(".popover-content input").each(function (i) {
            totalItens[i] = $(this).val();
        });
    });
    // spinner(+-btn to change value) & total to parent input 
    $(document).on('click', '.number-spinner a', function () {
        var btn = $(this),
        btnId = btn.attr('id'),
        productId = btnId.substr(btnId.lastIndexOf("-") + 1),
        input = btn.closest('.number-spinner').find('input'),
        total = $('#totalItens').val(),
        price = parseFloat($('#price-' + productId).html()),
        amountItens = parseFloat($('#amount').val()),
        oldValue = input.val().trim();

        if (btn.attr('data-dir') === 'up') {
            if (oldValue < input.attr('max')) {
                oldValue++;
                total++;
                amountItens += price;
            }
        } else {
            if (oldValue > input.attr('min')) {
                oldValue--;
                total--;
                amountItens -= price;
            }
        }
        $('#totalItens').val(total);
        $('#amount').val(amountItens);
        input.val(oldValue);
    });
    $(".popover-markup>.trigger").popover('show');
});