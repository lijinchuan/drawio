/**
 * Constructs a new point for the optional x and y coordinates. If no
 * coordinates are given, then the default values for <x> and <y> are used.
 * @constructor
 * @class Implements a basic 2D point. Known subclassers = {@link mxRectangle}.
 * @param {number} x X-coordinate of the point.
 * @param {number} y Y-coordinate of the point.
 */
ServerFile = function (ui, data, title) {
	DrawioFile.call(this, ui, data);

	this.title = title;
};

//Extends mxEventSource
mxUtils.extend(ServerFile, DrawioFile);

/**
 * Sets the delay for autosave in milliseconds. Default is 2000.
 */
ServerFile.prototype.autosaveDelay = 2000;

/**
 * Sets the delay for autosave in milliseconds. Default is 20000.
 */
ServerFile.prototype.maxAutosaveDelay = 20000;

/**
 * A differentiator of the stored object type (file or lib)
 */
ServerFile.prototype.type = 'F';

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.prototype.getMode = function () {
    return App.MODE_SERVER;
};

/**
 * Overridden to enable the autosave option in the document properties dialog.
 */
ServerFile.prototype.isAutosaveOptional = function () {
    return true;
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.prototype.getHash = function () {
    return 'V' + encodeURIComponent(this.getTitle());
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.prototype.getTitle = function () {
    return this.title;
};

ServerFile.prototype.getFileId = function () {
    return this.fileid||0;
};

ServerFile.prototype.setFileId = function (val) {
    this.fileid = val;
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.prototype.isRenamable = function () {
    return true;
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.prototype.save = function (revision, success, error) {
    this.saveAs(this.getTitle(), success, error);
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.prototype.saveAs = function (title, success, error) {
    DrawioFile.prototype.save.apply(this, arguments);
    this.saveFile(title, false, success, error);
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.insertFile = function (ui, title, data, success, error) {
    var createServerFile = mxUtils.bind(this, function (exists) {
        var fn = function () {

            var file = new ServerFile(ui, data, title);

            // Inserts data into local storage
            file.saveFile(title, false, function () {
                success(file);
            }, error);
        };

        if (exists) {
            ui.confirm(mxResources.get('replaceIt', [title]), fn, error);
        }
        else {
            fn();
        }
    });

    ServerFile.getFileContent(ui, title, function (data) {
        createServerFile(data != null);
    }, function () {
        createServerFile(false);
    });
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.getFileContent = function (ui, title, success, error) {

    mxUtils.post("/api/Drawio/FindByTitle", "Title=" + encodeURIComponent(title), function (req) {
        if (req.getStatus() >= 200 && req.getStatus() < 300) {
            var ret = JSON.parse(req.getText());
            success(ret.data ? ret.data.content : null);
        }
        else if (error != null) {
            error();
        }

    }, function (req) {
        if (error != null) {
            error();
        }
    });
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.getFileInfo = function (ui, title, success, error) {
    mxUtils.post("/api/Drawio/FindByTitle", "Title=" + encodeURIComponent(title), function (req) {
        if (req.getStatus() >= 200 && req.getStatus() < 300) {
            var ret = JSON.parse(req.getText());
            success(ret.data);
        }
        else if (error != null) {
            error();
        }

    }, function (req) {
        if (error != null) {
            error();
        }
    });
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.prototype.saveFile = function (title, revision, success, error) {
    if (!this.isEditable()) {
        if (success != null) {
            success();
        }
    }
    else {
        var fn = mxUtils.bind(this, function () {
            if (this.isRenamable()) {
                this.title = title;
            }

            try {
                var saveDone = mxUtils.bind(this, function () {
                    this.setModified(this.getShadowModified());
                    this.contentChanged();

                    if (success != null) {
                        success();
                    }
                });

                this.setShadowModified(false);
                var data = this.getData();
                var _this = this;
                mxUtils.post('/api/Drawio/SaveFile',
                    'FileId=' + this.getFileId() + '&title=' + encodeURIComponent(title) + '&' +
                    'content=' + encodeURIComponent(data), function (req) {
                        console.log(req);
                        if (req.getStatus() == 200) {
                            var ret = JSON.parse(req.getText());
                            if (ret.data > 0) {
                                _this.setFileId(ret.data);
                                saveDone();
                            } else {
                                if (error != null) {
                                    error();
                                }
                            }
                        } else {
                            if (error != null) {
                                error();
                            }
                        }
                    },
                    function () {
                        editorUi.alert('error saveFile');
                    });
            }
            catch (e) {
                if (error != null) {
                    error(e);
                }
            }
        });

        // Checks for trailing dots
        if (this.isRenamable() && title.charAt(0) == '.' && error != null) {
            error({ message: mxResources.get('invalidName') });
        }
        else {
            ServerFile.getFileInfo(this.ui, title, mxUtils.bind(this, function (data) {
                if (!this.isRenamable() || this.getTitle() == title || data == null) {
                    fn();
                }
                else {
                    this.ui.confirm(mxResources.get('replaceIt', [title]), fn, error);
                }
            }), error);
        }
    }
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.prototype.rename = function (title, success, error) {
    var oldTitle = this.getTitle();

    if (oldTitle != title) {
        ServerFile.getFileInfo(this.ui, title, mxUtils.bind(this, function (data) {
            var fn = mxUtils.bind(this, function () {

                var _this = this;

                ServerFile.PostApi('/api/Drawio/RenameFile', 'FileId=' + this.getFileId() + "&NewTitle=" + encodeURIComponent(title),
                    function (data) {
                        if (data) {
                            _this.title = title;
                            if (!_this.hasSameExtension(oldTitle, title)) {
                                _this.setData(_this.ui.getFileData());
                            }
                            _this.ui.removeLocalData(oldTitle, success);
                        } else {
                            error();
                        }
                }, error);

                this.title = title;
            });

            if (data != null) {
                this.ui.confirm(mxResources.get('replaceIt', [title]), fn, error);
            }
            else {
                fn();
            }
        }), error);
    }
    else {
        success();
    }
};

/**
 * Returns the location as a new object.
 * @type mx.Point
 */
ServerFile.prototype.open = function () {
    DrawioFile.prototype.open.apply(this, arguments);

    // Immediately creates the storage entry
    this.saveFile(this.getTitle());
};

/**
 * Adds the listener for automatically saving the diagram for local changes.
 */
ServerFile.prototype.getLatestVersion = function (success, error) {
    ServerFile.getFileContent(this.ui, this.title, mxUtils.bind(this, function (data) {
        success(new ServerFile(this.ui, data, this.title));
    }), error);
};

/**
 * Stops any pending autosaves and removes all listeners.
 */
ServerFile.prototype.destroy = function () {
    DrawioFile.prototype.destroy.apply(this, arguments);

    if (this.storageListener != null) {
        mxEvent.removeListener(window, 'storage', this.storageListener);
        this.storageListener = null;
    }
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.listFiles = function (ui, type, success, error) {

    ServerFile.PostApi('/api/Drawio/ListFiles', '', function (data) {
        var files = [];
        for (var i = 0; i < data.length; i++) {
            files.push({
                title: data[i].title,
                size: data[i].fileSize,
                type: 'F',
                hashKey: 'V',
                lastModified: new Date(data[i].updateTime).getTime()
            });
        }
        success(files);
    }, error);
};

/**
 * Translates this point by the given vector.
 * 
 * @param {number} dx X-coordinate of the translation.
 * @param {number} dy Y-coordinate of the translation.
 */
ServerFile.deleteFile = function (ui, title, success, error) {
    ServerFile.PostApi('/api/Drawio/DeleteFile', 'FileId=0&Title=' + encodeURIComponent(title), function () {
        success();
    }, error);
};

ServerFile.PostApi = function (url, formData, success, error) {
    mxUtils.post(url, formData, function (req) {
        if (req.getStatus() >= 200 && req.getStatus() < 300) {
            var ret = JSON.parse(req.getText());
            if (ret.code == 200) {
                success(ret.data);
            } else {
                error(ret.msg);
            }
        }
        else if (error != null) {
            error();
        }

    }, function (req) {
        if (error != null) {
            error();
        }
    });
};
