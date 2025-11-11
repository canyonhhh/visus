class DeviceFingerprintsController < ApplicationController
  before_action :set_device_fingerprint, only: %i[ show edit update destroy ]

  def index
    @device_fingerprints = DeviceFingerprint.all
  end

  def show
  end

  def new
    @device_fingerprint = DeviceFingerprint.new
  end

  def edit
  end

  def create
    @device_fingerprint = DeviceFingerprint.new(device_fingerprint_params)

    if @device_fingerprint.save
      redirect_to @device_fingerprint, notice: "Device fingerprint was successfully created."
    else
      render :new, status: :unprocessable_entity
    end
  end

  def destroy
    @device_fingerprint.destroy!

    redirect_to device_fingerprints_path, notice: "Device fingerprint was successfully destroyed.", status: :see_other
  end

  private
  def set_device_fingerprint
    @device_fingerprint = DeviceFingerprint.find(params.expect(:id))
  end

  def device_fingerprint_params
    params.expect(device_fingerprint: [ :fingerprint_value, :last_seen_at, :student_id ])
  end
end
