(function(window){
    // Compatibility wrapper to provide Swal.fire using sweetAlert (v1) provided in sweetalert.js
    if (window.Swal) return; // already defined (SweetAlert2)

    if (!window.swal && !window.sweetAlert) {
        console.warn('No sweetalert available to create Swal.wrapper');
        return;
    }

    window.Swal = {
        fire: function(options) {
            return new Promise(function(resolve){
                try {
                    // If options is a string, show simple alert and resolve confirmed
                    if (typeof options === 'string') {
                        swal(options);
                        resolve({ isConfirmed: true });
                        return;
                    }

                    var title = options.title || '';
                    var text = options.text || options.html || '';
                    var icon = options.icon || options.type || '';
                    var showCancel = options.showCancelButton === true || options.showCancelButton === undefined && options.cancelButtonText !== undefined;
                    var confirmText = options.confirmButtonText || 'OK';
                    var cancelText = options.cancelButtonText || 'Cancel';

                    if (showCancel) {
                        // sweetAlert v1 supports a callback with isConfirm
                        swal({
                            title: title,
                            text: text,
                            type: icon,
                            showCancelButton: true,
                            confirmButtonText: confirmText,
                            cancelButtonText: cancelText
                        }, function(isConfirm){
                            resolve({ isConfirmed: !!isConfirm });
                        });
                    } else {
                        swal({
                            title: title,
                            text: text,
                            type: icon
                        });
                        resolve({ isConfirmed: true });
                    }
                } catch (ex) {
                    console.error('Swal.compat error', ex);
                    resolve({ isConfirmed: false });
                }
            });
        }
    };

})(window);
