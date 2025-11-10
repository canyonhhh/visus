class QrCodesController < ApplicationController
  before_action :set_qr_code, only: %i[ show edit update destroy ]

  def index
    @qr_codes = QrCode.all
  end

  def show
  end

  def new
    @qr_code = QrCode.new
  end

  def edit
  end

  def create
    @qr_code = QrCode.new(qr_code_params)

    if @qr_code.save
      redirect_to @qr_code, notice: "Qr code was successfully created."
    else
      render :new, status: :unprocessable_entity
    end
  end

  def update
    if @qr_code.update(qr_code_params)
      redirect_to @qr_code, notice: "Qr code was successfully updated.", status: :see_other
    else
      render :edit, status: :unprocessable_entity
    end
  end

  def destroy
    @qr_code.destroy!

    redirect_to qr_codes_path, notice: "Qr code was successfully destroyed.", status: :see_other
  end

  private
  def set_qr_code
    @qr_code = QrCode.find(params.expect(:id))
  end

  def qr_code_params
    params.expect(qr_code: [ :token, :is_active, :activity_id ])
  end
end
