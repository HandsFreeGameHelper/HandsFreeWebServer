//export default class HandsFreeServiceCore {
//    constructor() { }

//    bindCommand() { 
//    }
//}

var HandsFreeService = HandsFreeService || {};

HandsFreeService.Core = HandsFreeService.Core || (function ()
{
    const setTempDataOnlyAsync = (argName, data) => {
        // requestId生成
        const uuid = _generateUuid();
        const subdomainId = $('[data-handsFreeService-subdomain]').attr('data-handsFreeService-subdomain');
        const currentScreenId = $('[data-handsFreeService-screenid]').attr('data-handsFreeService-screenid');

        const tempDataInfo = {
            key: argName,
            value: data,
            permanent: false
        };

        // リクエストデータ生成
        var requestData = {
            metadata: {
                requestId: uuid,
                system: 'HandsFreeService'
            },
            request: {
                currentScreenId: currentScreenId,
                currentUrl: window.location.href,
                tempDataInfo: tempDataInfo
            }
        };

        // API呼び出し（ページャで保持するデータを$.ajaxで保存するためのものなので、画面操作ログは保存しない）
        return connectWebApiAsync(`/uiapi/v1/AL1_AD002/`, 'POST', requestData, { skipOpeLog: true });
    };

    const _generateUuid = function () {
        let chars = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.split('');

        for (let i = 0; i < chars.length; i++) {
            switch (chars[i]) {
                case 'x':
                    chars[i] = Math.floor(Math.random() * 16).toString(16);
                    break;
                case 'y':
                    chars[i] = (Math.floor(Math.random() * 4) + 8).toString(16);
                    break;
            }
        }
        return chars.join('');
    };

    const connectWebApiAsync = function (url, method, requestData, options) {

        let opt = $.extend({ timeout: (1000 * 30), skipOpeLog: false }, options);

        if (typeof DevelopmentMode !== 'undefined' && DevelopmentMode) {
            opt = $.extend(opt, { timeout: 0 });
        }

        // リクエストデータをシリアライズ
        let requestJsonData = JSON.stringify(requestData);

        let deferred = $.Deferred();

        // 画面操作ログAPI呼び出し後の、実際のWEBAPI呼び出し。画面操作ログAPIの呼び出しで失敗した場合は、WEBAPIのerror処理に即座に入る。

        // WEBAPI呼び出し
        $.ajax({
            type: method,
            url: url,
            data: (method.toUpperCase() === 'GET') ? {
                d: requestJsonData
            } : requestJsonData,
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            dataType: 'json',
            cache: false,
            timeout: opt.timeout,
            success: function (data) {
                console.log("Success");
                deferred.resolve(data); // 呼び出し元ではdoneが実行される
            },
            error: function (data) {
                console.log("error");
                deferred.reject(data); // 呼び出し元ではfailが実行される
            }
        });

        return deferred.promise();
    };

    const getQueryString = function (url) {
        var vars = [], hash;
        var location = url ?? window.location.href;
        var hashes = location.slice(location.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            const key = hash[0];
            const value = hash[1];
            if (vars.hasOwnProperty(key)) {
                if (Array.isArray(vars[key])) {
                    vars[key].push(value);
                } else {
                    const val = vars[key];
                    vars[key] = new Array(val, value);
                }
            } else {
                vars.push(key);
                vars[key] = value;
            }
        }
        return vars;
    };

    return {
        //openTabAsync: openTabAsync,
        //displayToast: displayToast,
        //changeTextChangedStatus: changeTextChangedStatus,
        //checkTextDataUpdated: checkTextDataUpdated,
        //getTextDataUpdated: getTextDataUpdated,
        connectWebApiAsync: connectWebApiAsync,
        //connectWebApiWithRetryAsync: connectWebApiWithRetryAsync,
        //openModalAsync: openModalAsync,
        //closeModal: closeModal,
        //setModalData: setModalData,
        //outputErrorMessages: outputErrorMessages,
        //validationOrigin: validationOrigin,
        generateUuid: _generateUuid,
        setTempDataOnlyAsync: setTempDataOnlyAsync,
        getQueryString: getQueryString,
        //convertArrayFromObject: convertArrayFromObject,
        //showSpinner: showSpinner,
        //hideSpinner: hideSpinner,
        //suppressEnterKeyDown: suppressEnterKeyDown,
        //addFileBtnErrMonitoringEvent: addFileBtnErrMonitoringEvent,
        //registerAuditLog: registerAuditLog
    };
}());